using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour {

    public static QuestionController instance;

    private List<Question> _questions;
    private List<List<Answer>> _answers;
    private int questionIndex = 0;

    public int QuestionIndex
    {
        get
        {
            return questionIndex;
        }

        set
        {
            questionIndex = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void LoadQuestions()
    {
        _questions = new List<Question>();
        _answers = new List<List<Answer>>();
        

        List<Answer> answersList = new List<Answer>();

        _questions.Add(new Question(2, "Quel est la fonction Java qui permet de convertir une chaine de caractere en entier?", false));
        answersList.Add(new Answer(1, "int.parse()", false));
        answersList.Add(new Answer(2, "int.parseInt()", false));
        answersList.Add(new Answer(3, "Integer.parseInt()", true));
        answersList.Add(new Answer(4, "parseInt()", false));
        _answers.Add(answersList);


        _questions.Add(new Question(5, "What is the capital of Canada?", false));
        answersList = new List<Answer>();
        answersList.Add(new Answer(1, "Quebec", false));
        answersList.Add(new Answer(2, "Ottawa", true));
        answersList.Add(new Answer(3, "Toronto", false));
        answersList.Add(new Answer(4, "Rabat", false));
        _answers.Add(answersList);


        _questions.Add(new Question(14, "https://upload.wikimedia.org/wikipedia/commons/thumb/6/65/Flag_of_Belgium.svg/langfr-800px-Flag_of_Belgium.svg.png", true));
        answersList = new List<Answer>();
        answersList.Add(new Answer(1, "Belgium", true));
        answersList.Add(new Answer(3, "Norway", false));
        answersList.Add(new Answer(2, "Sweden", false));
        answersList.Add(new Answer(4, "Jamaica", false));
        _answers.Add(answersList);


        _questions.Add(new Question(6, "Which of the following elements are used to from water ?", false));
        answersList = new List<Answer>();
        answersList.Add(new Answer(1, "Hydrogen ", true));
        answersList.Add(new Answer(2, "Helium", false));
        answersList.Add(new Answer(3, "Carbon", false));
        answersList.Add(new Answer(4, "Chlorine", false));
        _answers.Add(answersList);

        _questions.Add(new Question(14, "https://www.biography.com/.image/t_share/MTQ1NDY2OTM4NTY5NTMzMjAx/freddie-mercury---musical-legacy.jpg", true));
        answersList = new List<Answer>();
        answersList.Add(new Answer(1, "Jack Johnson", false));
        answersList.Add(new Answer(3, "John Mayer", false));
        answersList.Add(new Answer(2, "Freddy Mercury", true));
        answersList.Add(new Answer(4, "Jason Mraz", false));
        _answers.Add(answersList);

        _questions.Add(new Question(7, "https://www.theflagshop.co.uk/media/catalog/product/cache/1/thumbnail/9df78eab33525d08d6e5fb8d27136e95/j/a/japan-flag-std.jpg   ", true));
        answersList = new List<Answer>();
        answersList.Add(new Answer(1, "Korea", false));
        answersList.Add(new Answer(2, "China", false));
        answersList.Add(new Answer(3, "France", false));
        answersList.Add(new Answer(4, "Japan", true));
        _answers.Add(answersList);

        _questions.Add(new Question(7, "Which city is the capital of England ?", false));
        answersList = new List<Answer>();
        answersList.Add(new Answer(1, "Rabat", false));
        answersList.Add(new Answer(2, "Manchester", false));
        answersList.Add(new Answer(3, "London", false));
        answersList.Add(new Answer(4, "Amsterdam", false));
        _answers.Add(answersList);

        _questions.Add(new Question(8, "What is the value of n: \nint n=0;\nn++;\nSystem.out.println(n++)", false));
        answersList = new List<Answer>();
        answersList.Add(new Answer(1, "1", true));
        answersList.Add(new Answer(2, "3", false));
        answersList.Add(new Answer(3, "2", false));
        answersList.Add(new Answer(4, "4", false));
        _answers.Add(answersList);

        _questions.Add(new Question(9, "What was the first sport to have a world championship?", false));
        answersList = new List<Answer>();
        answersList.Add(new Answer(1, "Billiards", true));
        answersList.Add(new Answer(2, "Swimming", false));
        answersList.Add(new Answer(3, "FootBall", false));
        answersList.Add(new Answer(4, "Karate", false));
        _answers.Add(answersList);


        _questions.Add(new Question(9, "Which city is the capital of Morocco ?", false));
        answersList = new List<Answer>();
        answersList.Add(new Answer(1, "Casablanca", false));
        answersList.Add(new Answer(2, "London", false));
        answersList.Add(new Answer(3, "Rabat", true));
        answersList.Add(new Answer(4, "Agadir", false));
        _answers.Add(answersList);

        _questions.Add(new Question(10, "Which city is the capital of Germany ?", false));
        answersList = new List<Answer>();
        answersList.Add(new Answer(1, "Düsseldorf", false));
        answersList.Add(new Answer(2, "Berlin", true));
        answersList.Add(new Answer(3, "Hamburg", false));
        answersList.Add(new Answer(4, "Munich", false));
        _answers.Add(answersList);

        _questions.Add(new Question(12, "Which planet is closet to the sun ?", false));
        answersList = new List<Answer>();
        answersList.Add(new Answer(1, "Venus", true));
        answersList.Add(new Answer(2, "Earth", false));
        answersList.Add(new Answer(3, "Venus", true));
        answersList.Add(new Answer(4, "Mars", false));
        _answers.Add(answersList);

        _questions.Add(new Question(13, "Which theory i known as the einstein theyory ?", false));
        answersList = new List<Answer>();
        answersList.Add(new Answer(2, "Theory of Relativity", true));
        answersList.Add(new Answer(1, "Evolution theory", false));
        answersList.Add(new Answer(3, "Thomas Jefferson", false));
        answersList.Add(new Answer(4, "Cell theyory", false));
        _answers.Add(answersList);


        _questions.Add(new Question(13, "How many colors are there in a rainbow?", false));
        answersList = new List<Answer>();
        answersList.Add(new Answer(1, "6", false));
        answersList.Add(new Answer(3, "4", false));
        answersList.Add(new Answer(2, "7", true));
        answersList.Add(new Answer(4, "6", false));
        _answers.Add(answersList);


    }
    public void Next()
    {
        GameUIController.instance.ShowQuestion(_questions[QuestionIndex], _answers[QuestionIndex]);
        GameUIController.instance.EnableBuzzer(true);
        GameUIController.instance.EnableAnswers(false);

        QuestionIndex++;
        
    }
    public void Next(int index)
    {
        GameUIController.instance.ShowQuestion(_questions[index], _answers[index]);
        GameUIController.instance.EnableBuzzer(true);
        GameUIController.instance.EnableAnswers(false);
        

    }
}
