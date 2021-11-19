import { HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AppFilesService } from 'src/app/services/api/appFiles.serive';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {

  @Output() onUploadFinished = new EventEmitter();
  @Output() fileName = new EventEmitter<string>();
  @Input() ButtonName: string = "Загруить файл";

  constructor(
    private service: AppFilesService,
    private toastr: ToastrService, 
  ) { }

  ngOnInit(): void {
  }

  uploadFile(files : FileList[] | any){
    if(files.length === 0)
      return;
    
    let fileToUpload = files[0];
    const formData = new FormData();
    formData.append('file',fileToUpload,fileToUpload.name);

    this.service.uploadFile(formData).toPromise().then(res => {
      if(res.type === HttpEventType.Response){
        this.fileName.emit(fileToUpload.name);
        this.onUploadFinished.emit(res.body);

        this.toastr.success('File uploaded!', 'Upload success.');
      }
    },
    err => {
      this.toastr.error('Failed uploaded!', 'Upload failed.');
      console.log(err)
    });
  }
}