import { BookDetails } from "./bookDetails";

export interface Book2 {
  id: string;
  title: string;
  author: string;
  isbn: string;
  image: string;
  bookDetails: BookDetails;
}
