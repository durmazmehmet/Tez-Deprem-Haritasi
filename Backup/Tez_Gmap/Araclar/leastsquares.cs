/*************************************************************************
Copyright (c) 2006-2007, Sergey Bochkanov (ALGLIB project).

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

- Redistributions of source code must retain the above copyright
  notice, this list of conditions and the following disclaimer.

- Redistributions in binary form must reproduce the above copyright
  notice, this list of conditions and the following disclaimer listed
  in this license in the documentation and/or other materials
  provided with the distribution.

- Neither the name of the copyright holders nor the names of its
  contributors may be used to endorse or promote products derived from
  this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*************************************************************************/

using System;
using Tez_Gmap.Araclar;

class leastsquares
{
    /*************************************************************************
    En küçük kareler yöntemi ile doðrusal kestirim (polyfit)

    Bu alt program verilen deðerlerle elde edilen doðru fonksiyonun katsayýlarýný verir

    Giriþ parametreleri
        X   -   array[0..N-1], absis deðerleri.
        Y   -   array[0..N-1], fonksiyon deðerleri.
        N   -   nokta sayýsý, N>=1

    Çýkýþ parametreleri:
        a, b-   doðrusal kestirimin katsayýlarý a+b*t

      -- ALGLIB --
         Copyright by Bochkanov Sergey
    *************************************************************************/
    public static void buildlinearleastsquares(ref double[] x,
        ref double[] y,
        int n,
        ref double a,
        ref double b)
    {
        double pp = 0;
        double qq = 0;
        double pq = 0;
        double b1 = 0;
        double b2 = 0;
        double d1 = 0;
        double d2 = 0;
        double t1 = 0;
        double t2 = 0;
        double phi = 0;
        double c = 0;
        double s = 0;
        double m = 0;
        int i = 0;

        pp = n;
        qq = 0;
        pq = 0;
        b1 = 0;
        b2 = 0;
        for (i = 0; i <= n - 1; i++)
        {
            pq = pq + x[i];
            qq = qq + AP.Math.Sqr(x[i]);
            b1 = b1 + y[i];
            b2 = b2 + x[i] * y[i];
        }        
        phi = Math.Atan2(2 * pq, qq - pp) / 2;
        c = Math.Cos(phi);
        s = Math.Sin(phi);
        d1 = AP.Math.Sqr(c) * pp + AP.Math.Sqr(s) * qq - 2 * s * c * pq;
        d2 = AP.Math.Sqr(s) * pp + AP.Math.Sqr(c) * qq + 2 * s * c * pq;
        if (Math.Abs(d1) > Math.Abs(d2))
        {
            m = Math.Abs(d1);
        }
        else
        {
            m = Math.Abs(d2);
        }
        t1 = c * b1 - s * b2;
        t2 = s * b1 + c * b2;
        if (Math.Abs(d1) > m * AP.Math.MachineEpsilon * 1000)
        {
            t1 = t1 / d1;
        }
        else
        {
            t1 = 0;
        }
        if (Math.Abs(d2) > m * AP.Math.MachineEpsilon * 1000)
        {
            t2 = t2 / d2;
        }
        else
        {
            t2 = 0;
        }
        a = c * t1 + s * t2;
        b = -(s * t1) + c * t2;
    }
}
