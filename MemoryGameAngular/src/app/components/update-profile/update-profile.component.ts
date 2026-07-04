//Angular
import { Component, EventEmitter, inject, Input, Output, signal } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FormControl, FormGroup, Validators } from '@angular/forms';

//Components
import { ModalComponent } from '../modal/modal.component';
import { GenericButtonComponent } from '../generic-button/generic-button.component';

//Models
import { UpdateProfile } from '../../models/components/UpdateProfile.model';

//i18n
import { TranslatePipe } from '@ngx-translate/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'update-profile-component',
  imports: [ModalComponent,TranslatePipe,GenericButtonComponent,ReactiveFormsModule],
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.css',
})

export class UpdateProfileComponent {
  @Input() isModalOpen !: boolean;
  @Input() initialData !: UpdateProfile;

  @Output() onClose = new EventEmitter<void>();
  @Output() onSaveProfile = new EventEmitter<UpdateProfile>();

  private translateService = inject(TranslateService);

  public formGroup !: FormGroup;
  public error = signal<string | null>('');

  ngOnInit() {
    this.formGroup = new FormGroup({
      username: new FormControl<string>(this.initialData.username, {
        nonNullable: true,
        validators: [Validators.minLength(5), Validators.required]
      }),
      email: new FormControl<string>(this.initialData.email , {
        nonNullable: true, 
        validators: [Validators.email, Validators.required]
      })
    })
  }

  handleCloseModal() : void {
    this.onClose.emit();
  }

  handleSaveProfile() : void {
    if(this.formGroup.valid) {
      this.onSaveProfile.emit(this.formGroup.getRawValue());
    } else {
      this.error.set(this.translateService.instant("Profile.SavingError"));
    }
  }
}
