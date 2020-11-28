using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace oopv16
{


    public class Lamp : Resistance
    {

        public delegate void ChangeBright(int brightlvl);

        // Яркость
        private double bright;
        public double Bright
        {
            get
            {
                return bright;
            }

            private set
            {
                if (value < 0) throw new ArgumentException();
                else bright = value;
            }
        }



        // Конструкторы

        public Lamp()
        : base() { }

        //public Lamp(double Temper, double U, double MinRes, double MaxRes,
        //    int ShowSpeed, int Step)
        //    : base(Temper, U, MinRes, MaxRes, ShowSpeed, Step) { }

        // Методы

        protected void DoBright(ShowMessage Print, ChangeBright changeBright)
        {
            Bright = BodyTem* Math.Pow(Math.Pow(Current*BodyRes,2)/MaxBodyRes/109,2) ;
            Print("Brightness", Convert.ToString(Math.Round(Bright, 1)));
            int Brightlvl = 255;
            if (Bright < 255 * 3)
            {
                Brightlvl = Convert.ToInt32(Bright / 3);
            }
            changeBright(Brightlvl);
        }

        virtual public void Start(ShowMessage Print, ChangeBright BrightLvl)
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
                    DoBright(Print, BrightLvl);
                }
            }
        }
    }
}
