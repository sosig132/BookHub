import { Category } from "./category";

  export interface BookCategories {
    id: string;
    $values: Array<Category>;
    categories: Category[];
  }
