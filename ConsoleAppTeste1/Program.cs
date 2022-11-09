
using System.IO.Ports;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleAppTeste1

{
   
    internal class Program
    {
        static SerialPort mySerialPort;
        static DadosSerial meusDados;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            mySerialPort = new SerialPort("COM7", 115200);
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(MySerialPort_DataReceived);

            meusDados = new DadosSerial("00:00:00", "Iniciando...","0" ,"0");

            try
            {
                mySerialPort.Open();
                Console.WriteLine("tudo certo!");
                while (true)
                {                    
                    Console.WriteLine($"Ciclo atual: {meusDados.GetCicloAtual()}");
                    Console.WriteLine($"Tensão de Bateria atual: {meusDados.GetTensaoDeBateria()}V");
                    Console.WriteLine($"Tempo atual: {meusDados.GetTempoDoCiclo()}");
                    Console.WriteLine($"Status do Sistema: {meusDados.GetStatusDoCiclo()}");
                    Console.WriteLine("Pressione uma tecla para atualizar!");
                    Console.ReadKey();
                    Console.Clear();
                } 
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
            
        }
        public static void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string leituraSerial = mySerialPort.ReadLine();
                if (leituraSerial.Contains("T: "))
                {
                    leituraSerial = leituraSerial.Remove(0, 3);
                    meusDados.SetTempoDoCiclo(leituraSerial);
                } 
                else if(leituraSerial.Contains("C: "))
                {
                    leituraSerial = leituraSerial.Remove(0, 3);
                    meusDados.SetCicloAtual(leituraSerial);
                }
                else if(leituraSerial.Contains("V: "))
                {
                    leituraSerial = leituraSerial.Remove(0, 3);
                    meusDados.SetTensaoDeBateria(leituraSerial);
                }
                else if(leituraSerial.Contains("S: "))
                {
                    leituraSerial = leituraSerial.Remove(0, 3);
                    meusDados.SetStatusDoCiclo(leituraSerial);
                }
                
                
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
        }
        class DadosSerial
        {
            private string TempoDoCiclo;
            private string StatusDoCiclo;
            private string CicloAtual;
            private string TensaoDeBateria;

            public DadosSerial(string tempoDoCiclo, string statusDoCiclo, string cicloAtual, string tensaoDeBateria)
            {
                TempoDoCiclo = tempoDoCiclo;
                StatusDoCiclo = statusDoCiclo;
                CicloAtual = cicloAtual;
                TensaoDeBateria = tensaoDeBateria;
            }
    
            public string GetCicloAtual()
            {
                return CicloAtual;
            }

            public void SetCicloAtual(string cicloAtual)
            {
                if(!string.IsNullOrEmpty(cicloAtual))
                {
                    if (Int32.Parse(cicloAtual) > 0)
                    {
                        CicloAtual = cicloAtual;
                    }
                    else
                    {
                        CicloAtual = "0";
                    }
                }
                else
                {
                    CicloAtual = "0";
                }
            }

            public string GetTensaoDeBateria()
            {
                return TensaoDeBateria;
            }

            public void SetTensaoDeBateria(string tensaoDeBateria)
            {
                if(!string.IsNullOrEmpty(tensaoDeBateria))
                { 
                    if (float.Parse(tensaoDeBateria) >= 0)
                    {
                        TensaoDeBateria = tensaoDeBateria;
                    } 
                    else
                    {
                        TensaoDeBateria = "0";
                    }
                }
                else
                {
                    TensaoDeBateria = "0";
                }
            }

            public string GetTempoDoCiclo()
            {
                return TempoDoCiclo;
            }

            public void SetTempoDoCiclo(string tempoDoCiclo)
            {
                if (!string.IsNullOrEmpty(tempoDoCiclo))
                {
                    TempoDoCiclo = tempoDoCiclo;
                }
                else
                {
                    TempoDoCiclo = "0";
                }
            }

            public string GetStatusDoCiclo()
            {
                return StatusDoCiclo;
            }

            public void SetStatusDoCiclo(string statusDoCiclo)
            {
                if (!string.IsNullOrEmpty(statusDoCiclo))
                {
                    StatusDoCiclo = statusDoCiclo;
                }
                else
                {
                    StatusDoCiclo = "0";
                }
            }
        }
    }
}