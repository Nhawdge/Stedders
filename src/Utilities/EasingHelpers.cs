namespace Stedders.Utilities
{
    internal static class EasingHelpers
    {
        public static double easeInSine(double x)
        {
            return 1 - Math.Cos((x * Math.PI) / 2);
        }

        public static double easeOutSine(double x)
        {
            return Math.Sin((x * Math.PI) / 2);
        }

        public static double easeInOutSine(double x)
        {
            return -(Math.Cos(Math.PI * x) - 1) / 2;
        }

        public static double easeInQuad(double x)
        {
            return x * x;
        }

        public static double easeOutQuad(double x)
        {
            return 1 - (1 - x) * (1 - x);
        }

        public static double easeInOutQuad(double x)
        {
            return x < 0.5 ? 2 * x * x : 1 - Math.Pow(-2 * x + 2, 2) / 2;
        }

        public static double easeInCubic(double x)
        {
            return x * x * x;
        }

        public static double easeOutCubic(double x)
        {
            return 1 - Math.Pow(1 - x, 3);
        }

        public static double easeInOutCubic(double x)
        {
            return x < 0.5 ? 4 * x * x * x : 1 - Math.Pow(-2 * x + 2, 3) / 2;
        }

        public static double easeInQuart(double x)
        {
            return x * x * x * x;
        }

        public static double easeOutQuart(double x)
        {
            return 1 - Math.Pow(1 - x, 4);
        }

        public static double easeInOutQuart(double x)
        {
            return x < 0.5 ? 8 * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 4) / 2;
        }

        public static double easeInQuint(double x)
        {
            return x * x * x * x * x;
        }

        public static double easeOutQuint(double x)
        {
            return 1 - Math.Pow(1 - x, 5);
        }

        public static double easeInOutQuint(double x)
        {
            return x < 0.5 ? 16 * x * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 5) / 2;
        }

        public static double easeInExpo(double x)
        {
            return x == 0 ? 0 : Math.Pow(2, 10 * x - 10);
        }

        public static double easeOutExpo(double x)
        {
            return x == 1 ? 1 : 1 - Math.Pow(2, -10 * x);
        }

        public static double easeInOutExpo(double x)
        {
            return x == 0
            ? 0
            : x == 1
            ? 1
            : x < 0.5 ? Math.Pow(2, 20 * x - 10) / 2
            : (2 - Math.Pow(2, -20 * x + 10)) / 2;
        }

        public static double easeInCirc(double x)
        {
            return 1 - Math.Sqrt(1 - Math.Pow(x, 2));
        }

        public static double easeOutCirc(double x)
        {
            return Math.Sqrt(1 - Math.Pow(x - 1, 2));
        }

        public static double easeInOutCirc(double x)
        {
            return x < 0.5
            ? (1 - Math.Sqrt(1 - Math.Pow(2 * x, 2))) / 2
            : (Math.Sqrt(1 - Math.Pow(-2 * x + 2, 2)) + 1) / 2;
        }

        public static double easeInBack(double x)
        {
            var c1 = 1.70158;
            var c3 = c1 + 1;
            return c3 * x * x * x - c1 * x * x;
        }

        public static double easeOutBack(double x)
        {
            var c1 = 1.70158;
            var c3 = c1 + 1;
            return 1 + c3 * Math.Pow(x - 1, 3) + c1 * Math.Pow(x - 1, 2);
        }

        public static double easeInOutBack(double x)
        {
            var c1 = 1.70158;
            var c2 = c1 * 1.525;
            return x < 0.5
               ? (Math.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
               : (Math.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
        }

        public static double easeInElastic(double x)
        {
            var c4 = (2 * Math.PI) / 3;
            return x == 0
                ? 0
                : x == 1
                ? 1
                : -Math.Pow(2, 10 * x - 10) * Math.Sin((x * 10 - 10.75) * c4);
        }

        public static double easeOutElastic(double x)
        {
            var c4 = (2 * Math.PI) / 3;
            return x == 0
            ? 0
            : x == 1
            ? 1
            : Math.Pow(2, -10 * x) * Math.Sin((x * 10 - 0.75) * c4) + 1;
        }

        public static double easeInOutElastic(double x)
        {
            var c5 = (2 * Math.PI) / 4.5;
            return x == 0
            ? 0
            : x == 1
            ? 1
            : x < 0.5
            ? -(Math.Pow(2, 20 * x - 10) * Math.Sin((20 * x - 11.125) * c5)) / 2
            : (Math.Pow(2, -20 * x + 10) * Math.Sin((20 * x - 11.125) * c5)) / 2 + 1;
        }

        public static double easeInBounce(double x)
        {
            return 1 - easeOutBounce(1 - x);
        }

        public static double easeOutBounce(double x)
        {
            var n1 = 7.5625;
            var d1 = 2.75;
            if (x < 1 / d1)
            {
                return n1 * x * x;
            }
            else if (x < 2 / d1)
            {
                return n1 * (x -= 1.5 / d1) * x + 0.75;
            }
            else if (x < 2.5 / d1)
            {
                return n1 * (x -= 2.25 / d1) * x + 0.9375;
            }
            else
            {
                return n1 * (x -= 2.625 / d1) * x + 0.984375;
            }
        }

        public static double easeInOutBounce(double x)
        {
            return x < 0.5
            ? (1 - easeOutBounce(1 - 2 * x)) / 2
            : (1 + easeOutBounce(2 * x - 1)) / 2;
        }
    }
}
