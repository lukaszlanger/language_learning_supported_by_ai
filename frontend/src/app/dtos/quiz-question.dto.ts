export class QuizQuestionDto {
  id: number | undefined;
  quizId: number | undefined;
  question: string | undefined;
  answers: string[] | undefined;
  correctAnswer: string | undefined;
  userAnswer?: string | undefined;
  isCorrect?: boolean | undefined;
}