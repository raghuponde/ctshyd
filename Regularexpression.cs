Regular expressions :
_________________________

How does pattern matches with the subject that is what we will see in out first program of regular expressions .

version 1 of the program 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            var pattern = Console.ReadLine();
            var subject = Console.ReadLine();

            var regex = new Regex(pattern);
            //once u make the instance of regex u cannot change it means if u want to make
            //use of different pattern another instance of regex has to be created .

            var match = regex.Match(subject);
            //if pattern is found in the subject it returns boolean value as true ..

            Console.WriteLine(match.Success);
            Console.ReadLine();


        }
    }
}

//note: options cat cat ,cat dog means dog is subject ,cat dogcat so it comes true why c is compared with d and then same c is compared 
//with o in dog and like that three times it fails and then c is  campared with c so it keeps moving its pattern to right untill its
//character in the pattern matches the character in the subject so then a to a and t to t will match okay once it succeeds it take other value
//does comaprison some more values i can try is cat dogcag this also results in false as pattern cat does not matched at the last character 
//so pattern is moved through the subject from left to right untill every character comparision succeeds or there is no subject left to 
//check .

version 2 of the program 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication5
{
    class Program
    {
        private const string MatchSuccess = "{0} @{1} : {2}";
        static void Main(string[] args)
        {
            var pattern = Console.ReadLine();
            var subject = Console.ReadLine();

            var regex = new Regex(pattern);
            //once u make the instance of regex u cannot change it means if u want to make
            //use of different pattern another instance of regex has to be created .

            var match = regex.Match(subject);
            //if pattern is found in the subject it returns boolean value as true ..

            if (match.Success)
            {

                Console.WriteLine(MatchSuccess, match.Success, match.Index, match.Length);

            }
            else
            {
                Console.WriteLine(match.Success);
            }
            Console.ReadLine();


        }
    }
}


enter cat cat ,then cat catcat etc and check it how it checks from left to right not right to left as i am entering two cat values okay.


so this was the feature which we disscussed related to concatening now we will move to alteration .


version 3 of the program

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication5
{
    class Program
    {
        private const string MatchSuccess = "{0} @{1} : {2}";
        static void Main(string[] args)
        {
            var pattern = Console.ReadLine();
            var subject = Console.ReadLine();

            var regex = new Regex(pattern);
            //once u make the instance of regex u cannot change it means if u want to make
            //use of different pattern another instance of regex has to be created .

            var match = regex.Match(subject);
            //if pattern is found in the subject it returns boolean value as true ..

            if (match.Success)
            {

                Console.WriteLine(MatchSuccess, match.Success, match.Index, match.Length);

            }
            else
            {
                Console.WriteLine(match.Success);
            }
            Console.WriteLine("second coding .......");
            var reg1 = new Regex("cat");
            var reg2 = new Regex("dog");
            var match1 = reg1.Match("dog");
            if (!match1.Success)
            {
                match1 = reg2.Match("dog");
                Console.WriteLine(match1.Success);
            }
            Console.WriteLine("second coding ...can be written like this....");
            var rx = new Regex("cat|dog");
            var macth3 = rx.Match("dog");
            Console.WriteLine(macth3.Success);//to check actually so like that i can combine as many patterns we can ...
            Console.ReadLine();


        }
    }
}

now enter input for first set of values like this :"cat|catnip" cat,"catnip|cat" cat,"catnip|cat" catnip so it compulsory to enter values in double quotes .


so till now two features we have seen concatenation and alteration next we will see repetiion okay 


now give the values like this  "a*"  a,"a*"  aaaa,  etc so here a* means for all repetion u show me the values .

also check "a*"  aaaaaaaaaaaaaaaaaa  etc so like this 

also check "a*"  zaaaaaaaaaaaaaaaaaaaaaaaaaa   this will also work and give true okay . this is giving true because * means o or many times . 


now imagine more than  one characters are there we can group them like this try below all possibilities and values okay .

(cat)* cat ,(cat)* catcat ,(cat)* dog this also will give as true as * indicated o or many times rememeber that so i grouped now as i dont have 

a only characters like earlier okay .

now i can give minumum and maximum matches 

(cat){1,4}  catcatcatcatcat  so this says that min occurence is one time and maximum is 4 . means it says that not that more cats even one is there 
it will match for example test the following below possibilities as well then u will understand why 4 does not work and all 

(cat){0,1} cat ,(cat){0,1} catcat ,(cat){0,1} dog so all three possibilitis will give me true u need to check the position where it  matching and all okay .

(cat){1,2}
catcatcatcatcatcatcatdogcatcat
True @0 : 6

so here eventhough i enter more cats which is against max 2 which i have defined it shows maxlength 6 only because cat is 3 characters and max is 2 defined so 3 * 2 is 6 okay .


For (cat){0,1} cat ,(cat){0,1} catcat ,(cat){0,1} dog  we can also use ? which means 0 or one time which will work fine okay 

(cat)? cat ,(cat)? catcat ,(cat)? dog okay is same as above and we will get the same output here hammm 


Then check the slide useof+ and try those possiblities as well so + means one time and how many times u write i will show the count .

Now i will use regular expressions to match the whole string finally before this check summary slide once to referesh til now what we have done okay .


suposse i want to find more than one match in the string then check slide code1idea,code2idea and code3idea and then write the follwoing code below given 



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication5
{
    class Program
    {
        private const string MatchSuccess = "{0} @{1} : {2}";
        static void Main(string[] args)
        {
            var pattern = Console.ReadLine();
            var subject = Console.ReadLine();

            var regex = new Regex(pattern);
            //once u make the instance of regex u cannot change it means if u want to make
            //use of different pattern another instance of regex has to be created .

            var match = regex.Match(subject);
            //if pattern is found in the subject it returns boolean value as true ..

           while  (match.Success)
            {

                Console.WriteLine(MatchSuccess, match.Success, match.Index, match.Length);
                match = match.NextMatch();

            }
            Console.WriteLine(match.Success);
            Console.WriteLine("second coding .......");
            var reg1 = new Regex("cat");
            var reg2 = new Regex("dog");
            var match1 = reg1.Match("dog");
            if (!match1.Success)
            {
                match1 = reg2.Match("dog");
                Console.WriteLine(match1.Success);
            }
            Console.WriteLine("second coding ...can be written like this....");
            var rx = new Regex("cat|dog");
            var macth3 = rx.Match("dog");
            Console.WriteLine(macth3.Success);//to check actually so like that i can combine as many patterns we can ...
            Console.ReadLine();


        }
    }
}

and input the values to above code from the slide tryoutcode and tryoutcode1 okay .Then u can see how ur code is working for multiple instances as well.


now let us go through internet and understand the regular expressions what each one is doing and all and as per the questions in dumps as well let us check.

so now type regular expressions in .net in google and do some study ask the students to do study with simple examples and then move on java2s.com as well and check some demos from there and then finally try to mug up checking email and password validation using regular expressions as well and this will end the regular expressions part as well .


Now again take the following program below try the following possibilites which i have mentioned and i have mentioned the reason as well 


we have got following tags \A,^,$,\Z,\z   so 

now \A is used for begining of string not for the line 

cat
dogcat
True is @3 :3
False


\Acat
dogcat
False


\Acat
catdog
True is @0 :3
False

\Acat
dog\ncat
False

so u can see eventhough it at the begining of the line it is showing false that is why on the top i have said at the begining of string not for lines 

^ works same as \A u can subsitute above values in stead of \A u will get the same output then what is the use of ^ operator 

so for multi line pattern it will work fine for example 

(?m)^cat
cat\ndog\ncat
True is @0 :3
False




(?m)\Acat
cat\ndog\ncat
True is @0 :3
False




for above statments i am not seeing the difference here so try this one 

u can look this site and put the expression then understand what id does from with an example is also given .

http://regexhero.net/reference/

http://www.codeproject.com/Articles/12452/Regular-Expressions-in-NET

http://www.codeproject.com/Articles/9099/The-Minute-Regex-Tutorial

(?m)^cat|^dog
cat\ndog\ncat\ndog
True is @0 :3
False

(?m:^cat)|^dog
cat\ndog\ncat
True is @0 :3
False

here also i am not getting what i want so 
switch on to some other attibute which is dollar sign 

end of the string or line purpose i will use $ sign let us check some examples 

cat$
cat
True is @0 :3
False

at the end of string cat is there 


cat$
catdog
False

it is giving false because at the end dog is there not cat .

(?m)cat$
cat\ndog\ncat\ndog\ncat
True is @20 :3
False


for multiline also it is working 

same task withhout using multiline means use 

cat\Z
cat\ndog\ncat
True is @10 :3
False


so it is perfectly working and small z does the same work as capital z but 

dog\z   dog  okay
dog\z   dog\n wrong
dog\n\z  dog\n okay 


so u can check it above by yourself. so small z is not for line as explained below 


\Z 	
The match must occur at the end of the string or before \n at the end of the string.
\z 	
The match must occur at the end of the string. 


so just put the character in 
http://regexhero.net/reference/ and see it explanation with an example best site to refer okay.


Let us move to now character classes :
______________________________________
[abc]
zzzc
True is @3 :1
False


same thing it works for [a|b|c]

[abc]?|(cat)+|[dc]og

so u can check how it works by putting in above code okay .

[a-z][0-9] for digits i can use and if u dont want that [^0-9A] etc 

\d matches any decimal integer 0 to 9 and \D is negation of that range [^0-9] what i means 

\s matches white spaces and \S is negation of that range 

\w represents any of the letter used in words like [a-z0-9A-Z] okay and \W is negation of that but in unicode tells that in  a word u can use other charcters as well okay .

special characters :
---------------------

\ 	In front of any of the special characters (. $ ^ { [ ( | ) * + ? \), this will match the character itself. 

\(dog\)\?
(dog)?
True is @0 :6
False


u can see here that when i dont use \ before special characters i will get true but at zero position okay .
so now i dont want to write \ every time so one inbuilt function is there to find out that for which coding is given down 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;



namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {

            var pattern = Console.ReadLine();
            var subject = Console.ReadLine();

             var escapepattern = Regex.Escape(pattern);
            Console.WriteLine("Escaped pattern :\" {0}\" ", escapepattern);
            var regex = new Regex(escapepattern);
            var match = regex.Match(subject);
            Console.WriteLine("{0} is @{1} :{2}", match.Success, match.Index, match.Length);
            Console.ReadLine();
        }
    }
}


and i got the output like this okay .


(dog)?
(dog)?
Escaped pattern :" \(dog\)\?"
True is @0 :6


so in this above example we have seen special characters as literal characters by escaping them okay.

dot is wild character in regular expressions and it can match any characters 


supply the following inputs now one by one 

. ----> a,z,Z,+,.,+,\n,abc so with dot try all these posiibilities and check 


for dot with abc is like this 

.
abc
True  @0:1
True  @1:1
True  @2:1
False


...  abc

the output for triple dots and abcd is like this 

...
abc
True  @0:3
False


with triple dot and abcdef 

...
abcdef
True  @0:3
True  @3:3
False


.{3}
abcdefghij
True  @0:3
True  @3:3
True  @6:3
False


for blank space we use \b

\b.{3}\b
abcdefghij
False

as no blank space in the subject so false 

\b.{3}\b
abc def ghij
True  @0:3
True  @4:3
False


\b.{3}\b
abc def gh
True  @0:3
True  @4:3
True  @7:3
False


so supply diferent values and see the corresponding output and try to understand how they are working and all.

above i am getting all the three values as true because it considering characters here including space so i want in terms of word means 

\b\w{3}\b
abc def gh
True  @0:3
True  @4:3
False


some more possiblities in between we will see 

c.t
cat
True  @0:3
False

c.t
cut
True  @0:3
False


c.t
cot
True  @0:3
False


c.t
cutcatcot
True  @0:3
True  @3:3
True  @6:3
False



some more :
------------
.*z means any no of characters but the last should be z


.*z
zaaaaa
True  @0:1
False


so why it is at position 0 and length one because after checking all characters from front left to right it does back track and after back tracking 
all the a 's it comes and see z is matching  so it is at zero positon and how many are matching means one so it gives the output like this okay .

Groups :
________

fish(cat(dog))bird

in this whole patteren is one group and then dog is one group and cat(dog) is one group so nested groups are not difined it thinks catdog as one another group 

fish(cat)*bird it looks for repeated patteren of cats 

fish(cat){3}bird it looks for repeated charcters of 3 times like that groups can be used .

in fish(cat)(dog)bird  here entire pattern is 0 group and cat is 1 group and dog is 2 group here and this is same as fish(cat(dog))bird okay as told nesting is not cosiderd in reular expressions 

so let use write one program to test it 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            var pattern = Console.ReadLine();
            var subject = Console.ReadLine();

            var regex = new Regex(pattern);
            
            var match = regex.Match(subject);
          
            while (match.Success)
            {

                Console.WriteLine("{0}  @{1}:{2}", match.Success, match.Index, match.Length);
                foreach ( var group in match.Groups)
                {
                    Console.WriteLine("{0}", group);
                    
                }
                match = match.NextMatch();

            }
            Console.WriteLine(match.Success);


            Console.ReadLine();

        }
    }
}


(\w+) meets (\w+)
sam meets jam
True  @0:13
sam meets jam
sam
jam
False

so this is the end of explanation now go though interent check some programs which have come in the exam 


so for pan car validation it is like this ^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$

so like this what and all i have told with that knowledge i am able to validate a pan card okay 

some more to explore is phonenos and email id of a person etc stuff so this finally ends the regular expressions okay .
