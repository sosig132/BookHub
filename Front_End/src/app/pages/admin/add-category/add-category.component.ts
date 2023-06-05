import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Category } from '../../../data/interfaces/category';
import { CategoryService } from '../../../services/category/category.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnInit {

  add: FormGroup;

  category: Category = {
    id: '',
    name: ''
  };

  constructor(private readonly router: Router, private formBuilder: FormBuilder, private readonly categoryService: CategoryService) { }

  ngOnInit(): void {
    this.add = this.formBuilder.group({
      name: ['', Validators.required]
    });   
  }

  get name() { return this.add.get('name') as FormControl }

  Add() {
    console.log(this.add.value);
    this.category = this.add.value;
    if (this.add.valid) {
      this.categoryService.postCategory(this.category).subscribe({
        next: category => {
          console.log(category);
          this.router.navigate(['add-book']);
        },
        error: err => {
          console.log(err);
        }
      });
    }
  }
}
