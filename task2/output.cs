public class Program2 
 {
public bool isPrime (int a, int b)
{
if (b<2)
{
return true;
}
else if (a%b == 0)
{
return false;
}
else
{
return isPrime(a,b-1);
}

}
public bool isPrime (int a)
{
return isPrime(a,a-1);
}

}
