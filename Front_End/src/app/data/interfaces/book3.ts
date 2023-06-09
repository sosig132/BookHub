import { BookCategories } from "./bookCategories";
import { BookDetails } from "./bookDetails";
import { Category } from "./category";

export interface Book3 {
  id: string;
  title: string;
  author: string;
  isbn: string;
  image: string;
  bookDetails: BookDetails;
  bookCategories: BookCategories;
}
