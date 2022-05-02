import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { EmailMessageSettingDto } from 'src/app/models/emailMessageSettings.models';
import { EmailMessageSettingService } from 'src/app/services/api/emailMessageSetting.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-email-message-settings-modal',
  templateUrl: './email-message-settings-modal.component.html',
  styleUrls: ['./email-message-settings-modal.component.css']
})
export class EmailMessageSettingsModalComponent implements OnInit {

  @ViewChild('emailMessageSettings', { static: true }) modal!: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  settings: EmailMessageSettingDto = new EmailMessageSettingDto();
  saving = false;
  showPswd = false;
  active = false;
  edit = false;


  constructor(private service: EmailMessageSettingService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  show(): void { 
    this.showPswd = false;
    this.edit = false;
    this.settings = new EmailMessageSettingDto();
    this.service.get().toPromise().then((res) => {
      if(res === null || res === undefined){
        this.settings = new EmailMessageSettingDto();
      }
      else
      {
        this.settings = res;
        this.edit = true;
      }
    });
    this.active = true;
    this.modal.show();
  }

  save(): void {
    this.saving = true; 

    if(this.edit === true){
      this.service.update(this.settings)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then(res =>{
        if(res){
          this.toastr?.success('Udpated!', 'Email Message Settings updated successful.');
          this.close();
          this.modalSave.emit(null);
        }else{
          this.toastr?.error('Failed!', 'Email Message Settings update failed.');
        } 
      });
    }else{
      this.service.create(this.settings)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then(
        (res: any) => {
          if(res){
            this.toastr?.success('Settings Created!', 'Created successful.');
            this.close();
            this.modalSave.emit(null);
          }else{
            this.toastr.error('Create Setting Failed','Create failed.');
          }
        },
        err => {
          console.log(err)
        }
      );
    }
  }

  remove(setting: EmailMessageSettingDto)
  {
    if(setting.id !== null && setting.id !== '' && setting !== null && setting !== undefined){
      Swal.fire({
        title: 'Удаление настроек',
        text: 'Вы Уверены ?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Ок',
        cancelButtonText: 'Отмена',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#7367F0',
      }).then((result) => {
        if (result.value) {
          this.service.remove(setting.id).toPromise().then(
            (res: any) => {
              if(res){
                this.toastr.success(`Settings Removed!`, 'Removed successful.');
                this.close();
              }else{
                this.toastr.error('Remove Settings Failed','Remove failed.');
              }
            },
            err => {
              this.toastr.error(err,'Remove failed.');
              console.log(err)
            }
          );
        } 
      })
    }
  }

  close(): void {
    this.modal.hide();
  }

  showHidePassword(){
    this.showPswd = !this.showPswd;
  }
}
