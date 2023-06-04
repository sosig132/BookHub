import { Role } from "../enums/role";

export interface User {
  id: string;
  username: string;
  password: string;
  displayName: string;
  email: string;
  role: Role;
  created_at: string;
  updated_at: string;
  is_banned: boolean;

}
