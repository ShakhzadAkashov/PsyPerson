import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { TestService } from 'src/app/services/api/test.service';

@Component({
  selector: 'app-create-test-questions-from-file-modal',
  templateUrl: './create-test-questions-from-file.component.html',
  styleUrls: ['./create-test-questions-from-file.component.css']
})
export class CreateTestQuestionsFromFileModalComponent implements OnInit {

  @ViewChild('createOrEditModal', { static: true }) modal!: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

  active = false;
  saving = false;

  buttonName: string = "Загрузить файл";
  instruction: string = "Можно загружать файлы с такими расширениями как: .txt, .doc, .docx; Шаблоны для загрузки файлов приведены ниже:";
  testId = '';
  fileToUpload: any;

  constructor(
    private toastr: ToastrService, 
    private service:TestService,
    public activatedRoute: ActivatedRoute,
  ) { 
    this.testId = this.activatedRoute.snapshot.queryParams['testId'];
  }

  ngOnInit(): void {
  }

  show(): void { 
    this.fileToUpload = '';

    this.active = true;
    this.modal.show();
  }

  save(): void {
    this.saving = true;

    const testQuestionsFromFile = new FormData();
    testQuestionsFromFile.append('file',this.fileToUpload,this.fileToUpload.name);
    testQuestionsFromFile.append('testId',this.testId);

    this.service.uploadTestQuestionsFromFile(testQuestionsFromFile)
    .pipe(finalize(() => { this.saving = false;}))
    .toPromise().then((res: any) =>{
      if(res){
        this.toastr.success('Created!', 'Test Questions From File successful created .');
        this.close();
        this.modalSave.emit(null);
      }
      else{
        this.toastr.error("Create Test Question From File Failed",'Create failed.');
      }
    },
    err => {
      this.toastr.error(err.message,'Create failed.');
      console.log(err)
    });
  }

  close(): void {
    this.active = false;
    this.modal.hide();
  }

  uploadFile(files : FileList[] | any){
    if(files.length === 0){
      this.toastr.error('File.Length = 0','File Upload failed.');
      return;
    }
      
    this.fileToUpload = files[0];

    if(this.fileToUpload){
      this.toastr.success('File uploaded!', 'Upload success.');
    }else
    {
      this.toastr.error('Upload File Failed','Uplaod Failed.');
    }
  }
}
