namespace RestWithASPNET.Configurations{ //armazena as configs do token
    public class TokenConfiguration{
        public string audience{get; set;}
        public string issuer{get; set;}
        public string secret{get; set;} //palavra usada pra cifrar

        public int minutes{get; set;}
        public int daystoExpire{get; set;}
    }
}