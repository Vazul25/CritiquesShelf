import { Inject, OnInit, Component, Input } from '@angular/core';
import { User } from '../../../models/User';
import { Review } from '../../../models/Review';
import { UserService } from '../../../services/user.service';
import { PageEvent } from '@angular/material';

@Component({
    selector: 'user',
    providers: [UserService],
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css'],
})

export class UserComponent implements OnInit {
    @Input() user: User;
    @Input() readOnly: boolean = false;

    public photoSource: any;
    public isEditing: boolean = false;
    public beforeEditUser: User;

    constructor(private userService: UserService) { }

    ngOnInit() {
        if (this.user.photo != null && this.user.photo != "") {
            this.setPhotoSource();
        }
    }

    onFileUpload(event: any) {
      var files = event.srcElement.files;
      this.getBase64(files[0]);
    }

    onEditClick() {
        console.log("OnEditClick! editing:", this.beforeEditUser);
        this.beforeEditUser = JSON.parse(JSON.stringify(this.user));
        this.isEditing = true;
    }

    onSaveClick() {
        console.log("OnSaveClick! editing:", this.isEditing);
        this.userService.saveUser(this.user).subscribe(data => {
            this.user = data as User;
        });
        this.isEditing = false;
    }

    onCancelClick() {
        console.log("OnCancelClick! editing:", this.beforeEditUser);
        this.user = JSON.parse(JSON.stringify(this.beforeEditUser));
        this.isEditing = false;
    }

    page(paging: PageEvent) {
      this.userService.getPagedUserReviews(this.user.id, paging.pageIndex, paging.pageSize).subscribe( data => {
          this.user.reviews = data as Review[];
      });
    }

    private getBase64(file: any) {
       var reader = new FileReader();
       reader.readAsDataURL(file);
       
       reader.onload = (e) => {
         this.user.photo = reader.result.split(';base64,')[1];
         this.setPhotoSource();
       };

       reader.onerror =  (e) => {
         console.log('Error: ', e);
       };
    }   

    private setPhotoSource() {
        this.photoSource = "data:image/JPEG;base64," + this.user.photo;
    }
}