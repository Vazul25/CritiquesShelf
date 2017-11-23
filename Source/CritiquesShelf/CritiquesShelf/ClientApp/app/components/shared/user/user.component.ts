import { Inject, OnInit, Component, Input } from '@angular/core';
import { User } from '../../../models/User';
import { UserService } from '../../../services/user.service';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';

@Component({
    selector: 'user',
    providers: [UserService],
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css'],
})

export class UserComponent {
    @Input() user: User;
    @Input() readOnly: boolean = false;

    private photoSource: any;
    private isEditing: boolean = false;

    constructor(private route: ActivatedRoute,http: Http, @Inject('BASE_URL') baseUrl: string, private userService: UserService) { }

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
        console.log("OnEditClick! editing:", this.isEditing);
        this.isEditing = true;
    }

    onSaveClick() {
        console.log("OnSaveClick! editing:", this.isEditing);
        this.isEditing = false;
    }

    onCancelClick() {
        console.log("OnCancelClick! editing:", this.isEditing);
        this.isEditing = false;
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