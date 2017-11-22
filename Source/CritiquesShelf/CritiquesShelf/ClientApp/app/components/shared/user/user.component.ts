import { Component, Input } from '@angular/core';
import { User } from '../../../models/User';

@Component({
    selector: 'user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css'],
})

export class UserComponent {
    @Input() user: User;
}