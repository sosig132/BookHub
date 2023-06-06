import { Role } from "../enums/role";

export interface User2 {
  id: string;
  username: string;
  password: string;
  displayName: string;
  email: string;
  role: Role;
  created_at: string;
  dateModified: string;
  isBanned: boolean;

}
