using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace oopv16 
{
    public class Heater : Lamp
    {
        Random rnd = new Random();


        // Изначальная температура в комнате
        private double initOutTem;
        public double InitOutTem
        {
            get
            {
                return initOutTem;
            }

            protected set
            {
                if (value < 200) throw new ArgumentException();
                else initOutTem = value;
            }
        }

        // События
        public override event VoidDelegate Update;
        public override event VoidDelegate OpMode;
        public override event VoidDelegate BurnOut;

        // Конструкторы

        public Heater()
        : base() { }

    //public Heater(double Temper, double U, double MinRes, double MaxRes,
    //    int ShowSpeed, int Step)
    //    : base(Temper, U, MinRes, MaxRes, ShowSpeed, Step) { InitOutTem = OutTem; }

    // Методы

    public override void Init(double Temper, double U, double MinRes, double MaxRes,
   int ShowSpeed, int Step)
        {
            if (Step < Math.Pow(10, ShowSpeed) || MinRes > MaxRes)
            {
                throw new ArgumentException();
            }
            PowerOn = true;
            DoStop = false;
            OperMode = false;
            Burned = false;
            this.Step = Step;
            this.ShowSpeed = ShowSpeed;
            MinBodyRes = MinRes;
            MaxBodyRes = MaxRes;
            BodyRes = MinBodyRes;
            Voltage = U;
            Current = Voltage / BodyRes;
            Power = Current * Voltage;
            OutTem = Temper;
            InitOutTem = Temper;
            BodyTem = OutTem;
            Work = 0;
            HeatLoss = 0;
            T = 0;

        }

        public override void Start(ShowMessage Print, ChangeBright BrightLvl)
        {
            if (Burned == false)
            {
                while (DoStop == false)
                {
                    if (PowerOn == true)
                    {
                        TickOn(Print);
                    }
                    else
                    {
                        TickOff(Print);
                    }
                }
            }
        }

        public new void TickOn(ShowMessage Print)
        {
            Update();
            Thread.Sleep(Convert.ToInt32(Step / Math.Pow(10, ShowSpeed)));
            T = T + Step;
            Work = Step * Power / 10000;
            HeatLoss = Step * 4.8 / MaxBodyRes;
            BodyTem = BodyTem + (Work - HeatLoss) * BodyTem * 0.001;
            BodyRes = MinBodyRes + 0.12 * BodyTem;
            Current = Voltage / BodyRes;
            if (Power == Current * Voltage & OperMode == false)
            {
                OpMode();
                OperMode = true;
            }
            Power = Current * Voltage;
            if (OutTem < InitOutTem + Power * BodyTem / 60800) OutTem = OutTem + 0.001;
            Print("BodyRes", Convert.ToString(Math.Round(BodyRes, 2)));
            Print("Current", Convert.ToString(Math.Round(Current, 2)));
            Print("Power", Convert.ToString(Math.Round(Power, 2)));
            Print("BodyTem", Convert.ToString(Math.Round(BodyTem, 2)));
            Print("OutTem", Convert.ToString(Math.Round(OutTem, 0)-273));
            Print("Time", Convert.ToString(Convert.ToDouble(T) / 1000));
            if (rnd.Next(0, 100000000) > 100000000 - Step)
            {
                BurnOut();
                Burned = true;
                DoStop = true;
            }
        }

        public new void TickOff(ShowMessage Print)
        {
            OperMode = false;
            Update();
            Thread.Sleep(Convert.ToInt32(Step / Math.Pow(10, ShowSpeed)));
            T = T + Step;
            HeatLoss = Step * 48.5 / MaxBodyRes;
            if (BodyTem > OutTem) BodyTem = BodyTem - HeatLoss * BodyTem * 0.001;
            BodyRes = MinBodyRes + 0.38 * BodyTem;
            Current = 0;
            Power = 0;
            if (OutTem > InitOutTem) OutTem = OutTem - 0.003;
            Print("BodyRes", Convert.ToString(Math.Round(BodyRes, 2)));
            Print("Current", Convert.ToString(Math.Round(Current, 2)));
            Print("Power", Convert.ToString(Math.Round(Power, 2)));
            Print("BodyTem", Convert.ToString(Math.Round(BodyTem, 2)));
            Print("OutTem", Convert.ToString(Math.Round(OutTem, 0) - 273));
            Print("Time", Convert.ToString(Convert.ToDouble(T) / 1000));
        }
    }
}
