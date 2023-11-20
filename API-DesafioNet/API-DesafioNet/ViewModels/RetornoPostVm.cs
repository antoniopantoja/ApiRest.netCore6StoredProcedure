using System.Runtime.Serialization.Json;
using System.Text;

namespace API_DesafioNet.ViewModels
{
    public class RetornoPostVm
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public int Output { get; set; }

        public RetornoPostVm()
        {
            Code = 500;
            Message = "Erro ao realizar a Requisição!";
            Output = 0;
        }

        public RetornoPostVm JSonParaObjectlist(string jsonString)
        {
            try
            {
                jsonString = jsonString.Substring(1, jsonString.Length - 2);
                jsonString = jsonString.Remove(0, 11);
                jsonString = jsonString.Substring(1, jsonString.Length - 2);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RetornoPostVm));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                RetornoPostVm obj = (RetornoPostVm)serializer.ReadObject(ms);

                if (obj != null)
                {
                    return new RetornoPostVm()
                    {
                        Output = obj.Output,
                        Message = obj.Message,
                        Code = obj.Code

                    };
                }
                else
                {
                    jsonString = "";
                }

                return new RetornoPostVm()
                {
                    Code = 1
                }; ;
            }
            catch
            {
                throw;
            }

        }

        public string JSonSubRemove(string jsonString)
        {
            try
            {
                jsonString = jsonString.Substring(1, jsonString.Length - 2);
                jsonString = jsonString.Remove(0, 11);

                return jsonString;
            }
            catch
            {
                throw;
            }

        }
    }
}
