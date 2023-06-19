using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    public abstract class MetroObject<T>
    {
        protected T status;
        public T Status
        {
            get { return status; }
            set { status = value; }
        }
        public abstract T CheckStatus();
        public abstract void ChangeStatus(T t);
    }
}
