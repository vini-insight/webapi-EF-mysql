namespace MyValidations
{
    public static class CpfString
    {
        public static bool Validar(string cpf)
        {
            if(cpf.Length == 11)
            {
                char[] chars = cpf.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    if (!char.IsDigit(chars[i]))
                        return false;
                }            
                return true;
            }
            else return false;
        }       
    }
}