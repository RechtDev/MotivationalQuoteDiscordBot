using System.ComponentModel;

namespace ProjectElectra.Common
{
    public class Enums
    {
        public enum Hours
        {
            [Description("12am")]
            TwelveAM = 0,

            [Description("1am")]
            OneAM,

            [Description("2am")]
            TwoAM,

            [Description("3am")]
            ThreeAM,

            [Description("4am")]
            FourAM,

            [Description("5am")]
            FiveAM,

            [Description("6am")]
            SixAM,

            [Description("7am")]
            SevenAM,

            [Description("8am")]
            EightAM,

            [Description("9am")]
            NineAM,

            [Description("10am")]
            TenAM,

            [Description("11am")]
            ElevenAM,

            [Description("12pm")]
            TwelvePM,

            [Description("1pm")]
            OnePM,

            [Description("2pm")]
            TwoPM,

            [Description("3pm")]
            ThreePM,

            [Description("4pm")]
            FourPM,

            [Description("5pm")]
            FivePM,

            [Description("6pm")]
            SixPM,

            [Description("7pm")]
            SevenPM,

            [Description("8pm")]
            EightPM,

            [Description("9pm")]
            NinePM,

            [Description("10pm")]
            TenPM,

            [Description("11pm")]
            ElevenPM,
        }

        public enum QuoteType
        {
            Success,
            Inspiration,
            Excellence,
            Confidence,
            Future,
            Work,
            Change
        }
    }
}