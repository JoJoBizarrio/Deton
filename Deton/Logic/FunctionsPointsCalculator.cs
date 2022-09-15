using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Deton.Fuels;

namespace Deton.Logic
{
    internal class FunctionsPointsCalculator
    {
        // {EXIT}
        double D; //GAMA1, MU0, MUMIN, MUMAX, E, KPA;

        // {TPR}
        double T, P, RO, MU, MUA, CE, CF, LI1, LI3, ENT; //, R  //
        double[] RIA = new double[5];

        // {ENTO} 
        double RO0, ENT0; // P0, ATM,  CALOR

        const double P0 = 1.0;
        const double R = 8314.41;
        const double ATM = 101325.0;
        const double Calor = 4.1868;

        // {LKP} 
        double[] LKP = new double[10];

        // {FEQ} 
        double FEQ1, FEQ2;

        // {HK}
        double[,] H = new double[19, 12];
        double[,] K = new double[19, 12];
        // {GF} 
        double GF;

        // {G}
        double GE;

        // {FUC}
        double FUCE1, FUCE2;
        byte avst;

        // {ENTR}

        double[] CA = new double[6];
        double[] HA = new double[6];
        double[] BENT = new double[6];
        double[] II = new double[9];

        // {param}
        double UCJ, FORCE, DX, TCJ, PCJ, ROCJ, MUCJ;
        double[] RI = new double[11];
        double[] RIRI = new double[10];

        private void SetHK()
        {
            // {O=0}
            H[0, 1] = -2.2933E-4;
            H[0, 2] = 1.9597E-2;
            H[0, 3] = 4.99429;
            H[0, 4] = -0.02955;
            H[0, 5] = -0.9995;
            H[0, 6] = 7.1506;
            H[0, 7] = -26.0162;
            H[0, 8] = 61.4432;
            H[0, 9] = -74.189;
            H[0, 10] = 34.31;
            H[0, 11] = 59548 - 1581;

            //{O2=1}
            H[1, 1] = -2.689E-4;
            H[1, 2] = 3.7752E-2;
            H[1, 3] = 5.20537;
            H[1, 4] = 30.47837;
            H[1, 5] = -146.8618;
            H[1, 6] = 421.1702;
            H[1, 7] = -584.5084;
            H[1, 8] = 132.5392;
            H[1, 9] = 525.966;
            H[1, 10] = -409.939;
            H[1, 11] = 0 - 2040;

            K[1, 0] = 0.2073E-4;
            K[1, 1] = -2.57848;
            K[1, 2] = 9.11264;
            K[1, 3] = -6.67363;
            K[1, 4] = 15.829;
            K[1, 5] = -29.6389;
            K[1, 6] = 29.0916;
            K[1, 7] = -0.4219;
            K[1, 8] = -24.562;
            K[1, 9] = 14.941;
            K[1, 11] = 1.04532;

            //{H=2}
            H[2, 1] = 0.0895E-4;
            H[2, 2] = -0.0706E-2;
            H[2, 3] = 4.98866;
            H[2, 4] = -0.27833;
            H[2, 5] = 1.9779;
            H[2, 6] = -7.7112;
            H[2, 7] = 16.3916;
            H[2, 8] = -17.2924;
            H[2, 9] = 6.023;
            H[2, 10] = 1.451;
            H[2, 11] = 52096 - 1456;

            //{H2=3}
            H[3, 1] = 1.2613E-4;
            H[3, 2] = -2.0862E-2;
            H[3, 3] = 7.60193;
            H[3, 4] = -11.88908;
            H[3, 5] = 102.3183;
            H[3, 6] = -315.2948;
            H[3, 7] = 435.2557;
            H[3, 8] = -98.3582;
            H[3, 9] = -361.534;
            H[3, 10] = 270.535;
            H[3, 11] = 0 - 1984;

            K[3, 0] = 0.1183E-4;
            K[3, 1] = -2.26099;
            K[3, 2] = 6.35191;
            K[3, 3] = 2.47658;
            K[3, 4] = -10.748;
            K[3, 5] = 21.8446;
            K[3, 6] = -21.989;
            K[3, 7] = 2.7874;
            K[3, 8] = 13.607;
            K[3, 9] = -8.355;
            K[3, 11] = 0.51912;

            //{OH=4}
            H[4, 1] = -1.856E-4;
            H[4, 2] = 0.5161E-2;
            H[4, 3] = 7.30835;
            H[4, 4] = -9.70431;
            H[4, 5] = 105.1446;
            H[4, 6] = -367.8405;
            H[4, 7] = 569.9797;
            H[4, 8] = -183.3183;
            H[4, 9] = -472.257;
            H[4, 10] = 395.277;
            H[4, 11] = 9318 - 2071;

            K[4, 0] = 0.038E-4;
            K[4, 1] = -2.21811;
            K[4, 2] = 6.17935;
            K[4, 3] = 2.05348;
            K[4, 4] = -11.3821;
            K[4, 5] = 26.755;
            K[4, 6] = -31.6665;
            K[4, 7] = 9.9422;
            K[4, 8] = 14.718;
            K[4, 9] = -11.224;
            K[4, 11] = 0.5845;

            //{H20=5}
            H[5, 1] = -3.961E-4;
            H[5, 2] = 3.456E-2;
            H[5, 3] = 6.8699;
            H[5, 4] = 10.3562;
            H[5, 5] = 65.58;
            H[5, 6] = -327.904;
            H[5, 7] = 566.943;
            H[5, 8] = -212.793;
            H[5, 9] = -445.63;
            H[5, 10] = 387.42;
            H[5, 11] = -57786 - 2328;

            K[5, 0] = -0.202E-4;
            K[5, 1] = -4.79032;
            K[5, 2] = 15.1064;
            K[5, 3] = -2.3913;
            K[5, 4] = -6.843;
            K[5, 5] = 23.284;
            K[5, 6] = -30.605;
            K[5, 7] = 10.471;
            K[5, 8] = 13.97;
            K[5, 9] = -10.93;
            K[5, 11] = 1.7708;

            //{CO=6}
            H[6, 1] = -7.163E-4;
            H[6, 2] = 6.28381E-2;
            H[6, 3] = 5.02467;
            H[6, 4] = 23.16888;
            H[6, 5] = -76.7948;
            H[6, 6] = 135.1824;
            H[6, 7] = -75.6202;
            H[6, 8] = -118.9548;
            H[6, 9] = 203.306;
            H[6, 10] = -86.358;
            H[6, 11] = -26425 - 2038;

            K[6, 0] = -0.504E-4;
            K[6, 1] = -5.58272;
            K[6, 2] = 9.23656;
            K[6, 3] = -4.86254;
            K[6, 4] = 7.3986;
            K[6, 5] = -7.0747;
            K[6, 6] = 0.6576;
            K[6, 7] = 7.2612;
            K[6, 8] = -7.846;
            K[6, 9] = 2.686;
            K[6, 11] = 1.06736;

            //{CO2=7}
            H[7, 1] = 7.474E-4;
            H[7, 2] = -4.994E-2;
            H[7, 3] = 6.8842;
            H[7, 4] = 55.2554;
            H[7, 5] = -222.559;
            H[7, 6] = 514.499;
            H[7, 7] = -557.259;
            H[7, 8] = -48.917;
            H[7, 9] = 663.79;
            H[7, 10] = -423.16;
            H[7, 11] = -94054 - 2195;

            K[7, 0] = 1.345E-4;
            K[7, 1] = -8.35985;
            K[7, 2] = 19.1101;
            K[7, 3] = -11.8812;
            K[7, 4] = 23.217;
            K[7, 5] = -34.186;
            K[7, 6] = 25.55;
            K[7, 7] = 6.886;
            K[7, 8] = -27.32;
            K[7, 9] = 14.27;
            K[7, 11] = 1.7524;

            //{N2=8}
            H[8, 1] = -7.8225E-4;
            H[8, 2] = 6.4682E-2;
            H[8, 3] = 5.10173;
            H[8, 4] = 20.50549;
            H[8, 5] = -60.28;
            H[8, 6] = 89.7273;
            H[8, 7] = -28.7247;
            H[8, 8] = -86.8081;
            H[8, 9] = 95.59;
            H[8, 10] = -22.473;
            H[8, 11] = 0 - 2037;

            K[8, 0] = -0.8883E-4;
            K[8, 1] = -4.90398;
            K[8, 2] = 8.87033;
            K[8, 3] = -4.78573;
            K[8, 4] = 7.7988;
            K[8, 5] = -9.7634;
            K[8, 6] = 5.572;
            K[8, 7] = 4.0956;
            K[8, 8] = -7.551;
            K[8, 9] = 2.923;
            K[8, 11] = 1.07605;

            //{NO=9}
            H[9, 1] = -7.935E-4;
            H[9, 2] = 7.9052E-2;
            H[9, 3] = 4.83286;
            H[9, 4] = 29.07911;
            H[9, 5] = -120.2217;
            H[9, 6] = 286.738;
            H[9, 7] = -334.5202;
            H[9, 8] = 32.0833;
            H[9, 9] = 308.769;
            H[9, 10] = -212.369;
            H[9, 11] = 21600 - 2158;

            K[9, 0] = -0.6333E-4;
            K[9, 1] = -3.26574;
            K[9, 2] = 8.53274;
            K[9, 3] = -6.51362;
            K[9, 4] = 13.6333;
            K[9, 5] = -21.9805;
            K[9, 6] = 18.8564;
            K[9, 7] = 1.4339;
            K[9, 8] = -15.983;
            K[9, 9] = 8.812;
            K[9, 11] = 1.13077;
        }

        private void FEQI()
        {
            RI[0] = Math.Exp(LI1);                                     // конц-ия равн-ная O
            RI[1] = Math.Exp(2 * LI1 - LKP[1]) * P;                    // конц-ия равн-ная О2
            RI[2] = Math.Exp(LI3);                                     // конц-ия равн-ная Н
            RI[3] = Math.Exp(2 * LI3 - LKP[3]) * P;                    // конц-ия равн-ная Н2
            RI[4] = Math.Exp(LI1 + LI3 - LKP[4]) * P;                  // конц-ия равн-ная ОН
            RI[5] = Math.Exp(LI1 + 2 * LI3 - LKP[5]) * P * P;          // конц-ия равн-ная Н2О

            MU = MUA / RIA[1] * (RI[2] + RI[4] + 2 * (RI[3] + RI[5]));

            double W = RI[0] * P * Math.Exp(LKP[6] - LKP[7]);

            RI[6] = RIA[2] * MU / MUA / (1.0 + W);                     // конц-ия равн-ная СО
            RI[7] = RI[6] * W;                                         // конц-ия равн-ная СО2

            double W1 = Math.Exp(2 * LKP[9] - LKP[8] - 2 * LI1) / P;
            double W2 = RIA[3] * MU / MUA;

            RI[9] = 2.0 * W2 / (Math.Sqrt(1.0 + 8.0 * W1 * W2) + 1.0); // конц-ия равн-ная N2
            RI[8] = RI[9] * W1 * RI[9];                                // конц-ия равн-ная NO
            RI[10] = RIA[4] * MU / MUA;                                // конц-ия равн-ная Ar

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
                    LKP[j] += Y * K[j, i];
                }

                Y *= X;
            }

            for (int i = 1; i <= 9; i++)
            {
                LKP[i] *= E;
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
            //int I1 = 0;

            for (int i = 0; i <= 600; i++)
            {
                FEQI();

                if (Math.Abs(FEQ1) + Math.Abs(FEQ2) < 0.00001)
                {
                    break;
                }

                //if (I1 > 600)
                //{
                //    avst = 1;
                //    break;
                //}

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

                //if (DT == 0)
                //{
                //    break;
                //}

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

            ENT = 0;

            double[] ENTJ = new double[20];

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
                    ENTJ[j] += Y * H[j, i];
                }

                Y *= X;
            }

            for (int i = 0; i <= 9; i++)
            {
                ENTJ[i] = 10000.0 * ENTJ[i] + H[i, 11];
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
                    ENTJ[j] += IY * Y * H[j, i];
                }

                Y *= X;
                IY += 1;
            }

            for (int i = 0; i <= 9; i++)
            {
                GF += RI[i] * ENTJ[i];
            }

            GF = GF / R * 4186.8 + 2.5 * RI[10];
            GF = GF / (GF - 1.0);
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

            double ED = 0.001;

            for (int i = 0; i <= 100; i++)
            {
                double PK = P;
                double TK = T;
                P = P * (1.0 + ED);
                FUCE();

                // if (avst != 0) then exit;
                // if BreakFlag then exit;

                double F1P = FUCE1;
                double F2P = FUCE2;

                P = PK;
                T = T * (1.0 + ED);
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

                if (Math.Abs(FUCE1) + Math.Abs(FUCE2) < 0.3E-5)
                {
                    return;
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
                    DP *= 0.1 / DPT;
                    DT *= 0.1 / DPT;
                }

                P += DP;
                T += DT;
            }

            avst = 2;
        }

        private void Detonation()
        {
            const double T0 = 298.15;
            const double Wair = 32.0 * 0.20954 + 28.016 * 0.78116 + 40.0 * 0.0093;

            LI1 = 0;
            LI3 = 0;

            double CN = II[0] * CA[0] + II[1] * CA[1] + II[2] * CA[2] + II[3] * CA[3]
               + II[4] * CA[4] + II[5] * CA[5];
            double HM = II[0] * HA[0] + II[1] * HA[1] + II[2] * HA[2] + II[3] * HA[3]
               + II[4] * HA[4] + II[5] * HA[5];

            double Weit1 = (12.011 * CA[0] + 1.008 * HA[0]) * II[0];
            double Weit2 = (12.011 * CA[1] + 1.008 * HA[1]) * II[1];
            double Weit3 = (12.011 * CA[2] + 1.008 * HA[2]) * II[2];
            double Weit4 = (12.011 * CA[3] + 1.008 * HA[3]) * II[3];
            double Weit5 = (12.011 * CA[4] + 1.008 * HA[4]) * II[4];
            double Weit6 = (12.011 * CA[5] + 1.008 * HA[5]) * II[5];
            double Weit = Weit1 + Weit2 + Weit3 + Weit4 + Weit5 + Weit6;

            Weit += 32.0 * II[6] + 28.016 * II[7] + 40.0 * II[9] + Wair * II[8];  // 6 - кислород, 7 - азот, 9 - аргон, 8 - воздух
            double All = II[0] + II[1] + II[2] + II[3] + II[4] + II[5] + II[6] + II[7] + II[8] + II[9];
            double MU0 = Weit / All;

            RO0 = P0 * ATM * MU0 / R / T0;

            double Alla = (CA[0] + HA[0]) * II[0];
            Alla += (CA[1] + HA[1]) * II[1];
            Alla += (CA[2] + HA[2]) * II[2];
            Alla += (CA[3] + HA[3]) * II[3];
            Alla += (CA[4] + HA[4]) * II[4];
            Alla += (CA[5] + HA[5]) * II[5];
            Alla += 2 * II[6] + 2 * II[7] + II[9] + (2.0 * 0.9907 + 0.0093) * II[8];

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

            ENT0 = ENT1 * II[0] + ENT2 * II[1] + ENT3 * II[2] + ENT4 * II[3] + ENT5 * II[4] + ENT6 * II[5];
            ENT0 = (ENT0 + ENT7 * (II[6] + 0.20954 * II[8]) + ENT8 * (0.78116 * II[8] + II[7]) + ENT10 * (II[9] + 0.0093 * II[8])) / Weit * Calor * 1000;
            UCE();

            // if BreakFlag then exit;

            UCJ = D - CE;
        }

        public double[] Detka(int j, Conditions conditions)
        {
            double[] functionsPoints = new double[18];

            SetHK();

            CA = new double[6];
            HA = new double[6];
            BENT = new double[6];
            II = new double[10];

            T = 0;
            P = 0;
            RO = 0;
            MU = 0;
            MUA = 0;
            CE = 0;
            CF = 0;
            LI1 = 0;
            LI3 = 0;
            ENT = 0; //, R
            D = 0;

            RO0 = 0;
            ENT = 0;
            ENT0 = 0;

            RIA = new double[5];

            LKP = new double[10];

            FEQ1 = 0;
            FEQ2 = 0;

            GF = 0;
            GE = 0;

            FUCE1 = 0;
            FUCE2 = 0;

            UCJ = 0;

            RI = new double[11];

            int k = j * 5;
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

            for (int i = 0; i < 3; i++)
            {
                II[i] = r1 * conditions.InitialMixture.ValuesArray[i];
                II[3 + i] = r2 * conditions.FinalMixture.ValuesArray[i];
            }

            II[6] = r1 * conditions.InitialMixture.ValuesArray[3] + r2 * conditions.FinalMixture.ValuesArray[3];
            II[8] = r1 * conditions.InitialMixture.ValuesArray[4] + r2 * conditions.FinalMixture.ValuesArray[4];
            II[7] = r1 * conditions.InitialMixture.ValuesArray[5] + r2 * conditions.FinalMixture.ValuesArray[5];
            II[9] = r1 * conditions.InitialMixture.ValuesArray[6] + r2 * conditions.FinalMixture.ValuesArray[6];

            for (int i = 0; i <= 2; i++)
            {
                CA[i] = conditions.InitialMixture.FuelsArray[i].CarbonAmount;
                CA[3 + i] = conditions.FinalMixture.FuelsArray[i].CarbonAmount;

                HA[i] = conditions.InitialMixture.FuelsArray[i].HydrogenAmount;
                HA[3 + i] = conditions.FinalMixture.FuelsArray[i].HydrogenAmount;

                BENT[i] = conditions.InitialMixture.FuelsArray[i].Enthalpy;
                BENT[3 + i] = conditions.FinalMixture.FuelsArray[i].Enthalpy;
            }

            Detonation();

            // if (avst != 0) then exit;
            // if BreakFlag then exit;

            functionsPoints[0] = D; // { DX}
            functionsPoints[1] = T; // { TCJ}
            functionsPoints[2] = P; // { PCJ}
            functionsPoints[3] = RO; //  { ROCJ}
            functionsPoints[4] = D - CE; // { UCJ}
            functionsPoints[5] = MU; // { MUCJ}
            functionsPoints[6] = RO * UCJ * UCJ / 2.0 / ATM; //  { FORCE}

            for (int i = 0; i <= 10; i++)
            {
                functionsPoints[7 + i] = RI[i];
            }

            return functionsPoints;
        }
    }
}