import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileExtV1Component } from './file-ext-v1.component';

describe('FileExtV1Component', () => {
  let component: FileExtV1Component;
  let fixture: ComponentFixture<FileExtV1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileExtV1Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FileExtV1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
