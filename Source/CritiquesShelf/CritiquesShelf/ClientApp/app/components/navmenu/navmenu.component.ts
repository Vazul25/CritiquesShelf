import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { CritiquesShelfRoles } from '../../models/CritiquesShelfRoles';
import { OnInit, OnDestroy } from '@angular/core';
@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    providers: [UserService],
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit {
    role: string;
    isAdmin: boolean=false;
    ngOnInit(): void {
        
        if (this.userService.userRole) {
            this.role = this.userService.userRole;
            this.isAdmin = this.checkIsAdmin();
            console.log(this.role);
        }
        else this.userService.getCurrentUserRole().subscribe(data => {
            this.role = data["role"];
            console.log(this.role);
            this.isAdmin = this.checkIsAdmin();
            
        });
  
    }
    checkIsAdmin(): boolean {
       
        if (this.role == CritiquesShelfRoles.Admin) return true;
        return false;  
    }


    constructor(private userService: UserService) { }
}
