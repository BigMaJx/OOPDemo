   ���һ�δ�����ܻ����ֱ��
   
   /// <summary>
    /// ����ʳ��
    /// </summary>
    public abstract class FoodAbstract
    {
        private string Name { get; set; }
        public FoodAbstract(string name) 
        {
            Name = name;
        }
        /// <summary>
        /// ׼���;�
        /// </summary>
        /// <returns></returns>
        public abstract string PrepareTheTableware();

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public abstract string Cook();

        /// <summary>
        /// ˢ��
        /// </summary>
        /// <returns></returns>
        private string BrushBowl() 
        {
            return "�����ˣ�ˢ��";
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string BeginEat()
        {            
            //����ط� �ͺܺõ������˶�̬
            //��ͬʳ��Ҫ�ò�ͬ�Ĳ;�
            //��ͬ��ʳ��Ҫ�ò�ͬ��⿷���
            //�������ǳ���󶼵�ˢ�롣Ϊ�˼����飬����Ĭ��ˢ����һ���Ĺ���

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Name);
            //��ò;�
            sb.AppendLine(PrepareTheTableware());
            //��ʼ���
            sb.AppendLine(Cook());
            //����ˢ��
            sb.AppendLine(BrushBowl());
            return sb.ToString();
        }
    }

         static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            BasicUsage();

            FactoryBasicUsage();
            Console.ReadLine();
        }

        //�����÷�
        static void BasicUsage()
        {
            Console.WriteLine("�����÷�");
            FoodAbstract foodAbstract = new Rice();
            Console.WriteLine(foodAbstract.BeginEat());
            foodAbstract = new Moodles();
            Console.WriteLine(foodAbstract.BeginEat());
        }

        /// <summary>
        /// �����÷�
        /// </summary>
        static void FactoryBasicUsage()
        {
            Console.WriteLine("�����÷�");
            var food = EatFactory.FoodFactory(FoodEnum.Moodles);
            Console.WriteLine(food.BeginEat());
            food = EatFactory.FoodFactory(FoodEnum.Rice);
            Console.WriteLine(food.BeginEat());
        }