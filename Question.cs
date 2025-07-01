Question 1
------------

James is working on a password generation feature for user accounts in an application.
He wants a program that takes the user's name, regardless of its case and generates
a password based on specific criteria. Help James to develop a C# program 
that takes the user's name as input, handles case-insensitivity, 
and generates a password according to the outlined conditions.



Constraints:

The password generation criteria are as follows:
The password must be at least 8 characters long.
When the user's name length is less than 8 characters, perform the following steps:
Convert the first letter of the user's name to uppercase and the rest to lowercase.
Add the special character "@" after the name.
Append digits at the end, add digits starting from 1 and incrementing by 1 until the length reaches at least 8 characters.
When the user's name is already 8 characters or more, display a message "Name must be less "
 

Note:

Do not edit the existing code template.
In the Sample Input / Output provided, the highlighted text in bold corresponds to the input given by the user, and the rest of the text represents the output.
Implement the business requirements within the main method. Please do not change the class name.
Do not use Environment.Exit() to terminate the program.
 

Sample Input / Output 2

Enter user name

bob

 

Generated Password: Bob@1234

 

Sample Input / Output 2

Enter user name

Charlotte

 

Name must be less than 8 characters


Question 2
----------

A chemical laboratory conducts various experiments, and it needs a system to track and manage the data for each experiment. Your objective is to develop a C# application to help them.

Functional Requirements:

-------------------------------------------------------------------------------------------------------------
| Req. # | Requirements Description                            | Class Name         | Method Name              | Parameters                        | Description                                                                                                      |
-------------------------------------------------------------------------------------------------------------
|   1    | Add the data to the list                            | ChemicalInventory  | RecordData               | Experiment data                   | This method adds the data to the ExperimentalData, which is implemented as a list. (already given in program     |
|        | provided in the program class                       |                    |                           |                                  | class).                                                                                                          |
|        |                                                     |                    |                           |                                  | Constraint:                                                                                                      |
|        |                                                     |                    |                           |                                  | - When the duration entered by the user is less than zero return false, otherwise return true when data is       |
|        |                                                     |                    |                           |                                  |   successfully added.                                                                                            |
-------------------------------------------------------------------------------------------------------------
|   2    | Calculate the Reaction rate                          | ChemicalInventory  | CalculateReactionRate    | List<Experiment> experimentalData| This method returns the Reaction rate (double value up to 4 decimal points)                                      |
|        |                                                     |                    |                           |                                  | Constraints:                                                                                                     |
|        |                                                     |                    |                           |                                  | - Sum all the duration.                                                                                          |
|        |                                                     |                    |                           |                                  | - Divide the count of the experimental data by the total duration.                                               |
-------------------------------------------------------------------------------------------------------------

Note:
You are provided with the main method in the Program class as code template, and it is excluded from evaluation.


Note:


-         Edit only the ChemicalInventory  class to implement the business requirements.

-         Keep all methods public.

-         Do not use Environment.Exit() to terminate the program.

-         Do not change the given code template.

-         In the Sample Input / Output provided, the highlighted text in bold corresponds to the input given by the user and the rest of the text represents the output.

 

Sample Input/Output 1:

1.To add the experiment

2.To find the Reaction rate

3.Exit

Enter your choice

1

Enter the Experiment name

Acid-Base Titration

Enter the duration in seconds

120



Experiment added successfully



1.To add the experiment

2.To find the Reaction rate

3.Exit

Enter your choice

1

Enter the Experiment name

Synthesis of Aspirin

Enter the duration in seconds

240



Experiment added successfully



1.To add the experiment

2.To find the Reaction rate

3.Exit

Enter your choice

1

Enter the Experiment name

Electrolysis of Water

Enter the duration in seconds

60



Experiment added successfully



1.To add the experiment

2.To find the Reaction rate

3.Exit

Enter your choice

2



Reaction rate is 0.0167



1.To add the experiment

2.To find the Reaction rate

3.Exit

Enter your choice

3



Thank you

 

Sample Input/output 2:

1.To add the experiment

2.To find the Reaction rate

3.Exit

Enter your choice

1

Enter the Experiment name

Synthesis of Aspirin

Enter the duration in seconds

-120



Enter the valid duration


Question 3
--------------
The City Library is home to thousands of books distributed across several genres. To efficiently manage the library’s collection and provide quick access to
book titles under different genres, the librarian needs a system. Help the librarian develop a Library Catalog Management System in C#.

Functional Requirement:

----------------------------------------------------------------------------------------------------------------------------------------------------------------------
| Req. # | Requirements Description                     | Class Name | Method Name       | Parameters                            | Description                                                                                                   |
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
|   1    | Add a book title to a specific genre         | Library    | AddBookToGenre    | string bookTitle, string genre        | This method should add the bookTitle to the genreCatalog, implemented as a Dictionary with genres as keys     |
|        |                                               |            |                    |                                     | and a LinkedList of book titles as values. (already provided in Program class)                                |
|        |                                               |            |                    |                                     |                                                                                                               |
|        |                                               |            |                    |                                     | Constraints:                                                                                                  |
|        |                                               |            |                    |                                     | - If the genre already exists, add the title at last to the existing LinkedList.                              |
|        |                                               |            |                    |                                     | - If not, a new genre is added as a key, and a new LinkedList is created with the title as its first element. |
|        |                                               |            |                    |                                     | - Genre names are case-sensitive.                                                                             |
|        |                                               |            |                    |                                     | - The return type is void.                                                                                    |
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
|   2    | Retrieve book titles under a given genre     | Library    | GetBooksInGenre   | string genre                          | Retrieves all book titles for the specified genre from the genreCatalog dictionary.                           |
|        |                                               |            |                    |                                     |                                                                                                               |
|        |                                               |            |                    |                                     | Constraints:                                                                                                  |
|        |                                               |            |                    |                                     | - If the genre exists, return the associated LinkedList.                                                      |
|        |                                               |            |                    |                                     | - If the genre is not found, returns an empty LinkedList.                                                     |
|        |                                               |            |                    |                                     | - Genre names are case-sensitive.                                                                             |
|        |                                               |            |                    |                                     | - The return type is LinkedList<string>.                                                                      |
----------------------------------------------------------------------------------------------------------------------------------------------------------------------


You are provided with the main method in the Program class as code template, and it is excluded from evaluation. 

 

Note: 

- Edit only the Library class to implement the business requirements. 

- Keep all methods public. 

- Do not use Environment.Exit() to terminate the program. 

- Do not change the given code template. 

- In the Sample Input / Output provided, the highlighted text in bold corresponds to the input given by the user and the rest of the text represents the output. 

 

Sample Input 1: 

Enter the number of books: 

5 

Enter the title of book 1: 

The Great Gatsby 

Enter the genre of book 1: 

Fiction 

Enter the title of book 2: 

A Brief History of Time 

Enter the genre of book 2: 

Science 

Enter the title of book 3: 

To Kill a Mockingbird 

Enter the genre of book 3: 

Fiction 

Enter the title of book 4: 

Cosmos 

Enter the genre of book 4: 

Science 

Enter the title of book 5: 

Hamlet 

Enter the genre of book 5: 

Drama 

Enter the genre to retrieve: 

Fiction 

 

Sample Output 1: 

Books under Fiction: 

The Great Gatsby 

To Kill a Mockingbird 

 

Sample Input 2: 

Enter the number of books: 

3 

Enter the title of book 1: 

1984 

Enter the genre of book 1: 

Dystopian 

Enter the title of book 2: 

The Road 

Enter the genre of book 2: 

Post-Apocalyptic 

Enter the title of book 3: 

Brave New World 

Enter the genre of book 3: 

Dystopian 

Enter the genre to retrieve: 

Mystery 

 

Sample Output 2: 

No books found in the given genre 


Next activity



Question 4 
----------


In the bustling port city of Seaview, managing cargo ship docking fees is a crucial task for efficient maritime operations. The DockMaster Hub has launched an automated 
CargoShip Docking System to streamline berth allocations and fee calculations.

This system allows ship captains to securely dock their vessels, calculate docking charges based on time spent, and manage payments seamlessly.

Functional Requirements:

--------------------------------------------------------------------------------------------------------------------------------------------------------------------
| Req. # | Requirements Description                          | Class Name | Method Name            | Parameters     | Description                                                                                                  |
--------------------------------------------------------------------------------------------------------------------------------------------------------------------
|   1    | Calculate docking charges for the time            | CargoDock  | CalculateDockingFee    | int hours      | This method calculates docking charges based on the number of hours docked.                                  |
|        | spent at the port.                                |            |                         |                | Returns the total docking fee as a double value.                                                             |
|        |                                                   |            |                         |                |                                                                                                              |
|        |                                                   |            |                         |                | Constraints:                                                                                                 |
|        |                                                   |            |                         |                | - Total docking charge is calculated by multiplying hours and location fee.                                  |
|        |                                                   |            |                         |                | - When location is Commercial Dock, fee per hour is 200.                                                     |
|        |                                                   |            |                         |                | - When location is Industrial Dock, fee per hour is 300.                                                     |
|        |                                                   |            |                         |                | - When location is Fishing Dock, fee per hour is 100.                                                        |
|        |                                                   |            |                         |                | - When location is not any of these locations, return 0.                                                     |
|        |                                                   |            |                         |                |                                                                                                              |
|        |                                                   |            |                         |                | Note: Location is Case-Sensitive.                                                                            |
--------------------------------------------------------------------------------------------------------------------------------------------------------------------
|   2    | Process payment for docking fees.                 | CargoDock  | ProcessPayment         | double amount  | This method validates the payment and updates the shipping company’s account balance.                        |
|        |                                                   |            |                         |                |                                                                                                              |
|        |                                                   |            |                         |                | Constraints:                                                                                                 |
|        |                                                   |            |                         |                | - If the docking charge is equal to or less than the account balance, deduct the charge and return           |
|        |                                                   |            |                         |                |   the updated balance.                                                                                       |
|        |                                                   |            |                         |                | - When the docking charge is greater than the account balance, return -1.                                    |
--------------------------------------------------------------------------------------------------------------------------------------------------------------------



CargoDock class needs to inherit from the abstract DockingSystem class (DockingSystem class is already provided in the code template)

You are provided with the main method in the Program class as code template, and it is excluded from evaluation.



Note:


-         Edit only the CargoDock class to implement the business requirements.

-         Keep all methods public.

-         Do not use Environment.Exit() to terminate the program.

-         Do not change the given code template.

-         In the Sample Input / Output provided, the highlighted text in bold corresponds to the input given by the user and the rest of the text represents the output.

 

Sample Input/Output 1:

Enter your account balance:

35000

Enter the number of hours docked:

15

Enter the docking location (Commercial Dock, Industrial Dock, Fishing Dock):

Industrial Dock



Docking charges: 4500



Do you want to proceed with the payment? (yes/no):

yes



Payment successful. Remaining account balance: 30500

 

Sample Input/Output 2:

Enter your account balance:

15000

Enter the number of hours docked:

10

Enter the docking location (Commercial Dock, Industrial Dock, Fishing Dock):

Commercial Dock



Docking charges: 2000



Do you want to proceed with the payment? (yes/no):

no



Transaction cancelled.



Sample Input/Output 3:

Enter your account balance:

50000

Enter the number of hours docked:

25

Enter the docking location (Commercial Dock, Industrial Dock, Fishing Dock):

Cosmetic Dock



Invalid hour or location



Question 5 
----------
Alex is organizing a gaming tournament, and he wants a program to find the first round where a player qualifies based on their performance. Write a C# program to assist Alex in creating a first qualifying round finder.

For example, Consider a gaming tournament with 5 rounds where a player scores 8, 5, 12, 7, and 13 points in each respective round. In this scenario, the first qualifying 
round is determined to be Round 3 because 12 is greater than any previous round's points (8, 5).



Constraints:

The player qualifies for the next round, display "Congratulations! The player has qualified for the next round with a first maximum point of <axPoint> in Round <qualifyingRound>".
When no qualifying round is found, the program will display the message "No qualifying round found".
 

Note:

Do not edit the existing code template.
In the Sample Input / Output provided, the highlighted text in bold corresponds to the input given by the user, and the rest of the text represents the output.
Implement the business requirements within the main method. Please do not change the class name.
Do not use Environment.Exit() to terminate the program.
 

Sample Input / Output 1

Enter the number of rounds

5

Enter the points for Round 1

8

Enter the points for Round 2

5

Enter the points for Round 3

12

Enter the points for Round 4

7

Enter the points for Round 5

13

 

Congratulations! The player has qualified for the next round with a first maximum point of 12 in Round 3

 

Sample Input / Output 2

Enter the number of rounds

1

Enter the points for Round 1

0

 

No qualifying round found.

 

 

