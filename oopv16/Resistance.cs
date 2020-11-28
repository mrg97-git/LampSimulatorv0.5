using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace oopv16
{
    
    public class Resistance
    {
        public delegate void ShowMessage(string name, string message);
        public delegate void VoidDelegate();

        Random rnd = new Random();

        // События
        public virtual event VoidDelegate Update;
        public virtual event VoidDelegate OpMode;
        public virtual event VoidDelegate BurnOut;

        // Свойства:
        // Напряжение
        private double voltage;
        public double Voltage
        {
            get
            {
                return voltage;
            }

            protected set
            {
                if (value < 0) throw new ArgumentException();
                else voltage = value;
            }
        }

        // Сопротивление тела накала
        private double bodyRes;
        public double BodyRes
        {
            get
            {
                return bodyRes;
            }

            protected set
            {
                if (value < 0) throw new ArgumentException();
                else bodyRes = value;
            }
        }

        // Минимальное сопротивление тела накала
        private double minBodyRes;
        public double MinBodyRes
        {
            get
            {
                return minBodyRes;
            }

            protected set
            {
                if (value < 0) throw new ArgumentException();
                else minBodyRes = value;
            }
        }

        // Максимальное сопротивление тела накала
        private double maxBodyRes;
        public double MaxBodyRes
        {
            get
            {
                return maxBodyRes;
            }

            protected set
            {
                if (value < 0) throw new ArgumentException();
                else maxBodyRes = value;
            }
        }

        // Ток через тело накала
        private double current;
        public double Current
        {
            get
            {
                return current;
            }

            protected set
            {
                if (value < 0) throw new ArgumentException();
                else current = value;
            }
        }

        // Температура тела накала
        private double bodyTem;
        public double BodyTem
        {
            get
            {
                return bodyTem;
            }

            protected set
            {
                if (value < 0) throw new ArgumentException();
                else bodyTem = value;
            }
        }

        // Температура среды
        private double outTem;
        public double OutTem
        {
            get
            {
                return outTem;
            }

            protected set
            {
                if (value < 200) throw new ArgumentException();
                else outTem = value;
            }
        }

        // Мощность
        private double power;
        public double Power
        {
            get
            {
                return power;
            }

            protected set
            {
                if (value < 0) throw new ArgumentException();
                else power = value;
            }
        }

        // Работа тока
        private double work;
        public double Work
        {
            get
            {
                return work;
            }

            protected set
            {
                if (value < 0) throw new ArgumentException();
                else work = value;
            }
        }

        // Потеря теплоты
        private double heatLoss;
        public double HeatLoss
        {
            get
            {
                return heatLoss;
            }

            protected set
            {
                if (value < 0) throw new ArgumentException();
                else heatLoss = value;
            }
        }

        // Время
        private int t;
        public int T
        {
            get
            {
                return t;
            }

            protected set
            {
                if (value < 0) throw new ArgumentException();
                else t = value;
            }
        }

        // Шаг
        private int step;
        public int Step
        {
            get
            {
                return step;
            }

            protected set
            {
                if (value < 0 || value > 10000) throw new ArgumentException();
                else step = value;
            }
        }

        // Ускорение просмотра
        private int showSpeed;
        public int ShowSpeed
        {
            get
            {
                return showSpeed;
            }

            protected set
            {
                if (value < -1 || value > 3) throw new ArgumentException();
                else showSpeed = value;
            }
        }


        // Режимы работы

        public bool DoStop { get; set; }

        public bool PowerOn { get; set; }

        public bool OperMode { get; set; }

        public bool Burned { get; set; }


        // Конструкторы:

        public Resistance()
        {
            PowerOn = true;
            DoStop = false;
            OperMode = false;
            Burned = false;
            Work = 0;
            HeatLoss = 0;
            T = 0;
        }

        //public Resistance(double Temper, double U, double MinRes, double MaxRes,
        //    int ShowSpeed, int Step)
        //{
        //    if (Step < Math.Pow(10, ShowSpeed) || MinRes > MaxRes)
        //    {
        //        throw new ArgumentException();
        //    }
        //    PowerOn = true;
        //    DoStop = false;
        //    OperMode = false;
        //    Burned = false;
        //    this.Step = Step;
        //    this.ShowSpeed = ShowSpeed;
        //    MinBodyRes = MinRes;
        //    MaxBodyRes = MaxRes;
        //    BodyRes = MinBodyRes;
        //    Voltage = U;
        //    Current = Voltage / BodyRes;
        //    Power = Current * Voltage;
        //    OutTem = Temper;
        //    BodyTem = OutTem;
        //    Work = 0;
        //    HeatLoss = 0;
        //    T = 0;

        //}

        // Методы

        public virtual void Init(double Temper, double U, double MinRes, double MaxRes,
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
            BodyTem = OutTem;
            Work = 0;
            HeatLoss = 0;
            T = 0;

        }

        public void Apply(int ShowSpeed, int Step)
        {
            if (Step < Math.Pow(10, ShowSpeed))
            {
                throw new ArgumentException();
            }
            this.Step = Step;
            this.ShowSpeed = ShowSpeed;
        }

        public void TickOn(ShowMessage Print)
        {
            Update();
            Thread.Sleep(Convert.ToInt32(Step / Math.Pow(10, ShowSpeed)));
            T = T + Step;
            Work = Step * Power / 100;
            HeatLoss = Step * 485 / MaxBodyRes;
            BodyTem = BodyTem + (Work - HeatLoss) * BodyTem * 0.001;
            BodyRes = MinBodyRes + 0.38 * BodyTem;
            Current = Voltage / BodyRes;
            if (Power == Current * Voltage & OperMode == false)
            {
                OpMode();
                OperMode = true;
            }
            Power = Current * Voltage;
            Print("BodyRes", Convert.ToString(Math.Round(BodyRes, 2)));
            Print("Current", Convert.ToString(Math.Round(Current, 2)));
            Print("Power", Convert.ToString(Math.Round(Power, 2)));
            Print("BodyTem", Convert.ToString(Math.Round(BodyTem, 2)));
            Print("Time", Convert.ToString(Convert.ToDouble(T) / 1000));
            if (rnd.Next(0, 10000000) > 10000000-Step)
            {
                BurnOut();
                Burned = true;
                DoStop = true;
            }
        }

        public void TickOff(ShowMessage Print)
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
            Print("BodyRes", Convert.ToString(Math.Round(BodyRes, 2)));
            Print("Current", Convert.ToString(Math.Round(Current, 2)));
            Print("Power", Convert.ToString(Math.Round(Power, 2)));
            Print("BodyTem", Convert.ToString(Math.Round(BodyTem, 2)));
            Print("Time", Convert.ToString(Convert.ToDouble(T) / 1000));
        }

        public void Start(ShowMessage Print, VoidDelegate Empty)
        {
            if (Burned == false)
            {
                while (DoStop == false)
                {
                    if (PowerOn == true) TickOn(Print);
                    else TickOff(Print);
                }
            }
        }


        public void Reset(double Temper, double U, double MinRes, double MaxRes,
            int ShowSpeed, int Step, ShowMessage Print)
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
            Current = 0;
            Power = 0;
            OutTem = Temper;
            BodyTem = OutTem;
            Work = 0;
            HeatLoss = 0;
            T = 0;
            Print("BodyRes", Convert.ToString(Math.Round(BodyRes, 2)));
            Print("Current", Convert.ToString(Math.Round(Current, 2)));
            Print("Power", Convert.ToString(Math.Round(Power, 2)));
            Print("BodyTem", Convert.ToString(Math.Round(BodyTem, 2)));
            Print("Time", Convert.ToString(Convert.ToDouble(t) / 1000));
        }

    }
}

