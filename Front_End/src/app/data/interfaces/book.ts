import { Category } from "./category";

export interface Book {
  id: string;
  title: string;
  author: string;
  description: string;
  isbn: string;
  image: string;
  publisher: string;
  language: string;
  pageCount: number;
  categories: Category[];
}
