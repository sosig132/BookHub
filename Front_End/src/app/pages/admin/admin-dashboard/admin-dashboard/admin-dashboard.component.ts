import { Component } from '@angular/core';
import { UsersService } from '../../../../services/users/users.service';
import { User } from '../../../../data/interfaces/user';
import { User2 } from '../../../../data/interfaces/user2';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
  constructor(private readonly userService: UsersService) {
    userService.getAllUsers().subscribe(
      response => {
        const serializedUsers = response as any; // Assuming response is the serialized data
        const users = serializedUsers.$values
          .filter((serializedUser: any) => serializedUser.role === 1)
          .map((serializedUser: any) => serializedUser as User);
        this.users = users;
      },
      error => {
        console.log(error);
      }
    );

  }
  displayedColumns: string[] = ['displayName', 'email', 'isBanned', 'actions'];

  users: User[];

  onButtonClick(user: User2) {
    // Handle the button click event for a specific user
    var userchanged: User2 = {
      id: user.id,
      displayName: user.displayName,
      email: user.email,
      password: user.password,
      role: user.role,
      isBanned: !user.isBanned,
      username: user.username,
      created_at: user.created_at,
      dateModified: user.dateModified

    };
    this.userService.updateUser(userchanged).subscribe({
      next: user => {
        console.log(user);
      },
      error: err => {
        console.log(err);
      }
    });

    window.location.reload();

}

  funci() {
    console.log(this.users);
  }


}
