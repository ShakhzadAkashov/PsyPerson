import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { UploadFileResponseDto } from 'src/app/models/appFiles.models';
import { TestDto, TestResultDto, TestResultStatusEnum, TestTypeEnum } from 'src/app/models/tests.models';
import { AppFilesService } from 'src/app/services/api/appFiles.serive';
import { TestService } from 'src/app/services/api/test.service';
import { GetTest } from 'src/app/store/actions/test.actions';
import { selectselectedTest } from 'src/app/store/selectors/test.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-create-or-edit-test-modal',
  templateUrl: './create-or-edit-test-modal.component.html',
  styleUrls: ['./create-or-edit-test-modal.component.css']
})
export class CreateOrEditTestModalComponent implements OnInit {

  @ViewChild('createOrEditModal', { static: true }) modal!: ModalDirective;
  @Output() modalSave: EventEmitter<TestDto> = new EventEmitter<TestDto>();
  test$: Observable<TestDto> = this.store.pipe(select(selectselectedTest));

  active = false;
  saving = false;
  edit = false;

  test: TestDto = new TestDto();
  uploadFileResponse : UploadFileResponseDto = new UploadFileResponseDto();
  imageToShow:any;

  testTypes :{key: any, value: TestTypeEnum}[] = [
    {
      key: 'Simple Test',
      value: TestTypeEnum.SimpleTest
    },
    {
      key: 'First Level Difficult Test',
      value: TestTypeEnum.FirstLevelDifficultTest
    },
    {
      key: 'Second Level Difficult Test',
      value: TestTypeEnum.SecondLevelDifficultTest
    }
  ]

  testResultStatuses :{key: any, value: TestResultStatusEnum}[] = [
    {
      key: 'Low',
      value: TestResultStatusEnum.Low
    },
    {
      key: 'Satisfactory',
      value: TestResultStatusEnum.Satisfactory
    },
    {
      key: 'Good',
      value: TestResultStatusEnum.Good
    },
    {
      key: 'Excelent',
      value: TestResultStatusEnum.Excelent
    }
  ]

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:TestService,
    private fileService: AppFilesService
  ) { }

  ngOnInit(): void {
  }

  show(test?: TestDto): void { 
    this.edit = false;
    this.uploadFileResponse = new UploadFileResponseDto();
    
    if (!test) {
        this.test = new TestDto();
        this.imageToShow = '';

        this.active = true;
        this.modal.show();
    } else {
        this.store.dispatch(new GetTest(test.id));
        this.test$.subscribe(res => {
          let r = res; 
          this.test.id = r.id;
          this.test.name = r.name;
          this.test.description = r.description;
          this.test.imgPath = r.imgPath;
          this.test.testType = r.testType;
          this.test.testResultList = [];
          r.testResultList.forEach(element =>{
            let x = new TestResultDto();
            x.id = element.id;
            x.name = element.name;
            x.rangeFrom = element.rangeFrom;
            x.rangeTo = element.rangeTo;
            x.status = element.status;
            x.testId = element.testId;
            this.test.testResultList.push(x);
          });
          this.createImgPath(r.imgPath);
        });

        this.edit = true;
        this.active = true;
        this.modal.show();
    }
  }

  save(): void {
    this.saving = true;

    if(this.uploadFileResponse !== null && this.uploadFileResponse !== undefined && this.uploadFileResponse.dbPath != null && this.uploadFileResponse.dbPath !==""){
      this.test.imgPath = this.uploadFileResponse.dbPath
    }

    if(this.edit == true){
      this.service?.update(this.test)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then((res: any) =>{
        if(res){
          this.toastr.success('Saved!', 'Test Saved successful.');
          this.close();
          this.modalSave.emit(this.test);
        }
        else{
          this.toastr.error("Update Test Failed",'Update failed.');
        }
      },
      err => {
        this.toastr.error("Update Test Failed",'Update failed.');
        console.log(err)
      });
    }else{
      this.service.create(this.test)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then(
        (res: any) => {
          if(res){
            this.toastr.success('New test created!', 'Created successful.');
            this.close();
            this.modalSave.emit(this.test);
          }
          else{
            this.toastr.error("Create Test Failed",'Created failed.');
          }
        },
        err => {
          this.toastr.error("Create Test Failed",'Created failed.');
          console.log(err)
        }
      );
    }
  }

  close(): void {
    this.active = false;
    this.modal.hide();
  }

  uploadFinished(event : any){
    this.uploadFileResponse = event;
    this.createImgPath(this.uploadFileResponse.dbPath);
  }

  createImgPath(filePath: string){
    if(filePath)
      this.imageToShow = this.fileService.getPhoto(filePath);
    else
      this.imageToShow =  '';
  }

  addTestResult(){
    var testResult = new TestResultDto();
    let max = 0;
    this.test.testResultList.forEach(obj => {
      if(obj.idForView > max){
        max = obj.idForView
      }
    });
    testResult.idForView = max + 1;
    testResult.id = '00000000-0000-0000-0000-000000000000';
    testResult.testId = '00000000-0000-0000-0000-000000000000';
    this.test.testResultList.push(testResult);
  }

  deleteTestResult(item: TestResultDto){
    this.test.testResultList = this.test.testResultList.filter(obj => obj.idForView !== item.idForView);
  }
}
