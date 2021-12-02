namespace AOC2021_Day2
{
    public class Submarine
    {
        public int HPos { get; set; } = 0;
        public int Depth { get; set; } = 0;
        public int Aim { get; set; } = 0;

        public void Move(string instruction)
        {
            var instParts = instruction.Split(' ');
            var dist = int.Parse(instParts[1]);
            switch (instParts[0])
            {
                case "forward":
                    HPos += dist;
                    Depth += dist * Aim;
                    break;
                case "down":
                    Aim += dist;
                    //Depth += dist;
                    break;
                case "up":
                    Aim -= dist;
                    //Depth -= dist;
                    break;
                default:
                    Console.WriteLine("Instruction NOK");
                    break;
            }
        }

        public int WhatsTheAnswer()
        {
            return HPos * Depth;
        }

    }
}
