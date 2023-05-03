using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globenar
{
    public interface IRegistration<T>
    {
        public T GetValue();
    }
}