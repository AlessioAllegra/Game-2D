using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IKillable
{
    void Kill();
    void Hurt(float damage);
}