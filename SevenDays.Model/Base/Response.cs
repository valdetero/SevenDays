using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Model.Base
{
    public class Response
    {
        public Response()
        {
            Successful = false;
        }

        public virtual bool Successful { get; set; }
        public string Message { get; set; }
    }

    public class Response<T> : Response where T : class, new()
    {
        public override bool Successful { get { return Result != null; } }

        public T Result { get; set; }
    }

    public class ListResponse<T> : Response where T : class, new()
    {
        public override bool Successful { get { return Result != null; } }

        public IEnumerable<T> Result { get; set; }
    }
}
