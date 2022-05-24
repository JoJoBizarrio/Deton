namespace Deton
{
    internal class DetonLogic
    {
        // {EXIT}
        double D, GAMA1, MU0, MUMIN, MUMAX, E, KPA;

        // {TPR}
        double T, P, RO, MU, MUA, CE, CF, LI1, LI3, ENT; //, R
        double[] RIA = new double[4];

        // {ENTO} 
        double RO0, P0, ENT0, CALOR; //, ATM

        const double p0 = 1.0;
        const double R = 8314.41;
        const double ATM = 101325.0;
        const double Calor = 4.1868;

        // {LKP} 
        double[] LKP = new double[9];

        // {FEQ} 
        double FEQ1, FEQ2;

        // {HK}
        double[,] H, K = new double[19, 11];

        // {GF} 
        double GF;

        // {G}
        double GE;

        // {FUC}
        double FUCE1, FUCE2;
        byte avst;

        // {ENTR}
        double[] CA, HA, BENT = new double[5];
        double[] II = new double[9];

        // {param}
        double UCJ, FORCE, DX, TCJ, PCJ, ROCJ, MUCJ;
        double[] RI, RIRI = new double[10];

        static private double EXP(double value)
        {
            return Math.Exp(value);
        }

        private void FEQI()
        {
            RI[0] = EXP(LI1);
            RI[1] = EXP(2 * LI1 - LKP[1]) * P;
            RI[2] = EXP(LI3);
            RI[3] = EXP(2 * LI3 - LKP[3]) * P;
            RI[4] = EXP(LI1 + LI3 - LKP[4]) * P;
            RI[5] = EXP(LI1 + 2 * LI3 - LKP[5]) * P * P;

            MU = MUA / RIA[1] * (RI[2] + RI[4] + 2 * (RI[3] + RI[5]));

            double W = RI[0] * P * EXP(LKP[6] - LKP[7]);

            RI[6] = RIA[2] * MU / MUA / (1.0 + W);
            RI[7] = RI[6] * W;

            double W1 = EXP(2 * LKP[9] - LKP[8] - 2 * LI1) / P;
            double W2 = RIA[3] * MU / MUA;

            RI[9] = 2.0 * W2 / (Math.Sqrt(1.0 + 8.0 * W1 * W2) + 1.0);
            RI[8] = RI[9] * W1 * RI[9];
            RI[10] = RIA[4] * MU / MUA;

            FEQ1 = (RI[0] + 2.0 * RI[1] - 0.5 * RI[2] - RI[3] + 0.5 * RI[4] + RI[6] + 2.0 * RI[7] + RI[9]) / (0.5 * RI[2] + RI[3] + 0.5 * RI[4] + RI[5]);

            if (Math.Abs(FEQ1) < 0.001)
            {
                FEQ1 = Math.Log(RIA[1] / (2.0 * RIA[0])) + FEQ1 * (1.0 - 0.5 * FEQ1);
            }
            else
            {
                FEQ1 = Math.Log(RIA[1] * (1.0 + FEQ1) / (2.0 * RIA[0]));
            }

            FEQ2 = 0;

            for (int i = 0; i <= 10; i++)
            {
                FEQ2 += RI[i];
            }

            FEQ2 = Math.Log(FEQ2);
        }

        private void LKPI(double T)
        {
            double X = T * 0.0001;
            double XL = Math.Log(X);
            double Y = 1.0 / (X * X);
            double E = Math.Log(10);

            for (int i = 1; i <= 9; i++)
            {
                LKP[i] = K[i, 11] * XL;
            }

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    LKP[j] = LKP[j] + Y * K[j, i]; // LKP[j]=LKP[j]+Y*K[J,I]; - тут заглавные буквы индексов, че?
                    Y *= X;
                }
            }

            for (int i = 1; i <= 9; i++)
            {
                LKP[i] = LKP[i] * E;
            }

            LKP[2] = 0;
        }

        private void EN()
        {
            avst = 0;

            if (LI1 == 0)
            {
                LI1 = -3.0;
            }

            if (LI3 == 0)
            {
                LI3 = -3.0;
            }

            double DL = 1.0E-3;
            LKPI(T);
            int I1 = 0;

            do
            {
                I1++;
                FEQI();

                if (Math.Abs(FEQ1) + Math.Abs(FEQ2) < 0.00001)
                {
                    break;
                }

                if (I1 > 600)
                {
                    avst = 1;
                    break;
                }

                double KLI1 = LI1;
                double KLI3 = LI3;
                double FEQ1K = FEQ1;
                double FEQ2K = FEQ2;
                LI1 = KLI1 + DL;
                FEQI();

                double D11 = (FEQ1 - FEQ1K) / DL;
                double D21 = (FEQ2 - FEQ2K) / DL;
                LI1 = KLI1;
                LI3 = KLI3 + DL;
                FEQI();

                double D12 = (FEQ1 - FEQ1K) / DL;
                double D22 = (FEQ2 - FEQ2K) / DL;
                double DT = D11 * D22 - D12 * D21;
                double D1 = (FEQ2K * D12 - FEQ1K * D22) / DT;
                double D3 = (FEQ1K * D21 - FEQ2K * D11) / DT;

                double AD = Math.Abs(D1) + Math.Abs(D3);

                if (AD >= 0.1)
                {
                    D1 = D1 / AD * 0.1;
                    D3 = D3 / AD * 0.1;
                }

                LI1 = KLI1 + D1;
                LI3 = KLI3 + D3;

                // if BreakFlag then exit;
            }
            while (I1 <= 600);

            double[] ENTJ = new double[19];

            for (int i = 0; i <= 9; i++)
            {
                ENTJ[i] = 0;
            }

            double X = T * 0.0001;
            double Y = 1.0 / X;

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    ENTJ[j] = ENTJ[j] + Y * H[j, i];
                }

                Y *= X;
            }

            for (int i = 0; i <= 9; i++)
            {
                ENTJ[i] = 10000.0 * ENTJ[i] + H[i, 12];
                ENT += RI[i] * ENTJ[i];
            }

            ENT = (ENT * 4186.8 + RI[10] * 2.5 * R * T) / MU;
            RO = MU * P * 101325 / 8314.41 / T;
            GF = 0;

            for (int i = 0; i <= 9; i++)
            {
                ENTJ[i] = 0;
            }

            Y = 1.0 / (X * X);
            double IY = -1.0;

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    ENTJ[j] = ENTJ[j] + IY * Y * H[j, i];
                }

                Y *= X;
                IY += 1;
            }

            for (int i = 0; i <= 9; i++)
            {
                GF += RI[i] * ENTJ[i];
            }

            GF /= R * 4186.8 + 2.5 * RI[10];
            GF /= GF - 1.0;
        }

        private void EQI()
        {
            double PK = P;
            double TK = T;

            P *= 1.01;
            EN();

            // if (avst != 0) then exit;
            // if BreakFlag then exit;

            double ENTP1 = ENT;
            double ROP1 = RO;

            P = PK * 0.99;
            EN();

            // if (avst != 0) then exit;
            // if BreakFlag then exit;

            double ENTP = ENT;
            double ROP = RO;
            P = PK;
            T *= 1.01;
            EN();

            // if (avst != 0) then exit;
            // if BreakFlag then exit;

            double ENTT1 = ENT;
            double ROT1 = RO;
            T = TK * 0.99;
            EN();

            double ENTT = ENT;
            double ROT = RO;
            T = TK;
            EN();

            // if (avst != 0) then exit;
            // if BreakFlag then exit;

            double ENTRO = (ENTT1 - ENTT) / (ROT1 - ROT);
            ENTP = (ENTP1 - ENTP - ENTRO * (ROP1 - ROP)) / (0.02 * P * 101325);
            double CE2 = RO * ENTRO / (1.0 - RO * ENTP);
            CE = Math.Sqrt(CE2);
            GE = CE2 * RO / P / 101325;
        }

        private void FUCE()
        {
            EQI();

            // if (avst != 0) then exit;
            // if BreakFlag then exit;

            D = RO * CE / RO0;
            FUCE1 = (1 + GE) / (P0 * ATM + RO0 * D * D) * P * ATM - 1;
            FUCE2 = (ENT + CE * CE / 2 - ENT0 - D * D / 2) * 1.0E-7;
        }

        private void UCE()
        {
            double PKN = 30.0 * P0;
            double TKN = 4000.0;

            P = PKN;
            T = TKN;

            int i = 0;
            double ED = 0.001;

            do
            {
                double PK = P;
                double TK = T;
                i++;
                P *= 1.0 + ED;
                FUCE();

                // if (avst != 0) then exit;
                // if BreakFlag then exit;

                double F1P = FUCE1;
                double F2P = FUCE2;

                P = PK;
                T *= 1.0 + ED;
                FUCE();

                // if (avst != 0) then exit;
                // if BreakFlag then exit;

                double F1T = FUCE1;
                double F2T = FUCE2;
                T = TK;
                FUCE();

                // if BreakFlag then exit;

                double S = RO / RO0;

                // if (avst != 0) then exit;
                if ((Math.Abs(FUCE1) + Math.Abs(FUCE2)) < 0.3E-5)
                {
                    // then exit; 
                }

                F1P = (F1P - FUCE1) / P / ED;
                F2P = (F2P - FUCE2) / P / ED;
                F1T = (F1T - FUCE1) / T / ED;
                F2T = (F2T - FUCE2) / T / ED;

                double DET = F1P * F2T - F2P * F1T;
                double DP = -(FUCE1 * F2T - FUCE2 * F1T) / DET;
                double DT = (FUCE1 * F2P - FUCE2 * F1P) / DET;
                double DPT = Math.Abs(DP) / P + Math.Abs(DT) / T;

                if (DPT >= 0.1)
                {
                    DP = DP * 0.1 / DPT;
                    DT = DT * 0.1 / DPT;
                }

                P += DP;
                T += DT;
            }
            while (i < 100);

            avst = 2;
        }

        private void Detonation()
        {
            const double T0 = 298.15;
            const double Wair = 32.0 * 0.20954 + 28.016 * 0.78116 + 40.0 * 0.0093;

            LI1 = 0;
            LI3 = 0;

            double CN = II[1] * CA[1] + II[2] * CA[2] + II[3] * CA[3] + II[4] * CA[4]
               + II[5] * CA[5] + II[6] * CA[6];
            double HM = II[1] * HA[1] + II[2] * HA[2] + II[3] * HA[3] + II[4] * HA[4]
               + II[5] * HA[5] + II[6] * HA[6];

            double Weit1 = (12.011 * CA[0] + 1.008 * HA[0]) * II[0];
            double Weit2 = (12.011 * CA[1] + 1.008 * HA[1]) * II[1];
            double Weit3 = (12.011 * CA[2] + 1.008 * HA[2]) * II[2];
            double Weit4 = (12.011 * CA[3] + 1.008 * HA[3]) * II[3];
            double Weit5 = (12.011 * CA[4] + 1.008 * HA[4]) * II[4];
            double Weit6 = (12.011 * CA[5] + 1.008 * HA[5]) * II[5];
            double Weit = Weit1 + Weit2 + Weit3 + Weit4 + Weit5 + Weit6;
            Weit = Weit + 32.0 * II[6] + 28.016 * II[7] + 40.0 * II[9] + Wair * II[8];
            double All = II[0] + II[1] + II[2] + II[3] + II[4] + II[5] + II[6] + II[7] + II[8] + II[9];
            double MU0 = Weit / All;
            RO0 = P0 * ATM * MU0 / R / T0;

            double Alla = (CA[0] + HA[0]) * II[0];
            Alla += (CA[1] + HA[1]) * II[1];
            Alla += (CA[2] + HA[2]) * II[2];
            Alla += (CA[3] + HA[3]) * II[3];
            Alla += (CA[4] + HA[4]) * II[4];
            Alla += (CA[5] + HA[5]) * II[5];
            Alla = Alla + 2 * II[7] + 2 * II[8] + II[10] + (2.0 * 0.9907 + 0.0093) * II[9];
            MUA = Weit / Alla;
            RIA[0] = 2 * (II[6] + 0.20954 * II[8]) / Alla;
            RIA[1] = HM / Alla;
            RIA[2] = CN / Alla;
            RIA[3] = 2 * (II[7] + 0.78116 * II[8]) / Alla;
            RIA[4] = (II[9] + 0.0093 * II[8]) / Alla;

            double ENT7 = 49.0065;
            double ENT8 = 2037 * (298.15 / 293.15 - 1.0);
            double ENT10 = 2.5 * 1.987 * T0;
            double ENT1 = BENT[0] * 1000.0 / Calor;
            double ENT2 = BENT[1] * 1000.0 / Calor;
            double ENT3 = BENT[2] * 1000.0 / Calor;
            double ENT4 = BENT[3] * 1000.0 / Calor;
            double ENT5 = BENT[4] * 1000.0 / Calor;
            double ENT6 = BENT[5] * 1000.0 / Calor;
            double ENT0 = ENT1 * II[0] + ENT2 * II[1] + ENT3 * II[2] + ENT4 * II[3] + ENT5 * II[4] + ENT6 * II[5];
            ENT0 = (ENT0 + ENT7 * (II[6] + 0.20954 * II[6]) + ENT8 * (0.78116 * II[8] + II[7]) + ENT10 * (II[9] + 0.0093 * II[8])) / Weit * Calor * 1000;

            UCE();

            // if BreakFlag then exit;

            UCJ = D - CE;
        }

        private void Detka(int v, int j)
        {
            int k = (j - 1) * 5;
            double r1 = 1;
            double r2 = 1;

            if (k == 100)
            {
                r1 = 0;
            }
            else
            {
                r2 = 1.0 * k / (100 - k);
            }

            for (int i = 0; i <= 2; i++)
            {
                II[i] = r1 * Alfa[v, i];
                II[3 + i] = r2 * Beta[v, i];
            }

            II[7] = r1 * Alfa[v, 4] + r2 * Beta[v, 4];
            II[8] = r1 * Alfa[v, 6] + r2 * Beta[v, 6];
            II[9] = r1 * Alfa[v, 5] + r2 * Beta[v, 5];
            II[10] = r1 * Alfa[v, 7] + r2 * Beta[v, 7];
            for (int i = 0; i <= 2; i++)
            {
                CA[i] = MixTray[i].prm[1];
                CA[3 + i] = DilTray[i].prm[1];
                HA[i] = MixTray[i].prm[2];
                HA[3 + i] = DilTray[i].prm[2];
                BENT[i] = MIxTray[i].prm[3];
                BENT[3 + i] = DilTray[i].prm[3];
            }
            Detonation();

            // if (avst != 0) then exit;
            // if BreakFlag then exit;

            Fun[v, 0, j] = D; // { DX}
            Fun[v, 1, j] = T; // { TCJ}
            Fun[v, 2, j] = P; // { PCJ}
            Fun[v, 3, j] = RO; //  { ROCJ}
            Fun[v, 4, j] = D - CE; // { UCJ}
            Fun[v, 5, j] = MU; // { MUCJ}
            Fun[v, 6, j] = RO * UCJ * UCJ / 2.0 / ATM; //  { FORCE}

            for (int i = 0; i <= 10; i++)
            {
                Fun[v, 6 + i, j] = RI[i];
            }
        }
    }
}