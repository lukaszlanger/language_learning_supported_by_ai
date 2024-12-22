import { QuizQuestionDto } from "./quiz-question.dto";

export class QuizDto {
  id: number | undefined;
  lessonId: number | undefined;
  quizQuestions: QuizQuestionDto[] | undefined;
}