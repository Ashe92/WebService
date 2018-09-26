"use strict";
var protocol = "http://";
var serverAddress = "localhost";
var port = ":49989";// ":27017";
var apiPath = "/api/";

var studentsList;
var studentCoursesList;
var marksList;
var coursesList;
var studentCourseMarkList;

var studentEditor;
var studentDetails;
var studentCourseEditor;
var courseEditor;
var courseDetails;
var markDetails;
var marksEditor;

var StudentModel = function () {
    var self = this;

    this.Id = ko.observable();
    this.Index = ko.observable();
    this.Name = ko.observable();
    this.Surname = ko.observable();
    this.BirthDate = ko.observable();
    this.Method = ko.observable();
    this.Courses = ko.observableArray();

    this.isEdit = ko.computed(function () {
        if (this.Id())
            return true;

        return false;
    }, this);
};

var CourseModel = function() {
    var self = this;

    this.Id = ko.observable();
    this.CourseName = ko.observable();
    this.LeadTeacher = ko.observable();
    this.Points = ko.observable();

    this.isEdit = ko.computed(function ()
    {
        if (this.Id())
            return true;

        return false;
    }, this);
};

var MarkModel = function() {
    var self = this;

    this.Id = ko.observable();
    this.StudentId = ko.observable();
    this.CourseId = ko.observable();
    this.AddedDate = ko.observable();
    this.Evaluation = ko.observable();
}

var StudentCourseModel = function () {
    var self = this;

    this.parentId = ko.observable();
    this.list = ko.observableArray();
    this.selectedCourse = ko.observable();
};

var ListViewModel = function() {
    var self = this;
    this.list = ko.observableArray();
    this.parentId = ko.observable();

    this.IndexFilter = ko.observable();
    this.NameFilter = ko.observable();
    this.SurnameFilter = ko.observable();
    this.BirthdateFilter = ko.observable();
    this.MethodFilter = ko.observable();

    this.CourseNameFilter = ko.observable();
    this.LeadTeacherFilter = ko.observable();
    this.PointsFilter = ko.observable(); 

    this.StudentIdFilter = ko.observable(); 
    this.CourseIdFilter = ko.observable(); 
    this.AddedDateFilter = ko.observable(); 
    this.EvaluationFilter = ko.observable(); 
};

var ErrorModel = function () {
    this.Message = ko.observable();
    this.ExceptionMessage = ko.observable();
};

$(document).ready(function() {
    coursesList = new ListViewModel();
    studentsList = new ListViewModel();
    studentCoursesList = new ListViewModel();
    marksList = new ListViewModel();
    studentCourseMarkList = new ListViewModel();

    studentEditor = new StudentModel();
    studentDetails = new StudentModel();
    studentCourseEditor = new StudentCourseModel();
    courseDetails = new CourseModel();
    courseEditor = new CourseModel();

    markDetails = new MarkModel();


    ko.applyBindings(studentsList, $("#students")[0]);
    ko.applyBindings(studentEditor, $("#student-editor")[0]);
    ko.applyBindings(studentDetails, $("#student-details")[0]);

    ko.applyBindings(studentCourseEditor, $("#student-courses-add")[0]);

    ko.applyBindings(coursesList, $("#courses")[0]);
    ko.applyBindings(courseDetails, $("#course-details")[0]);
    ko.applyBindings(courseEditor, $("#course-editor")[0]);

    ko.applyBindings(marksList, $("#marks")[0]);
    


    $(".studentsFilter").focusout(function () {
        GetStudents();
    });
    $(".coursesFilter").focusout(function () {
        GetCourses();
    });
    $(".marksFilter").focusout(function () {
        GetMarks();
    });

    $('#student-form').submit(function (e) {
        GetStudents();
        window.location.href = "#students";
    });

    $('#course-form').submit(function (e) {
        GetCourses();
        window.location.href = "#courses";
    });

    $('#mark-form').submit(function (e) {
        GetCourses();
        window.location.href = "#marks";
    });

    $('#student-course-add').submit(function (e) {
        GetStudentCourses(studentCoursesList.parentId());
        window.location.href = "#student-courses";
    });

});

function MapStudentVM(vm1, vm2) {
    vm1.Id(vm2.Id);
    vm1.Index(vm2.Index());
    vm1.Name(vm2.Name());
    vm1.Surname(vm2.Surname());
    vm1.BirthDate(vm2.BirthDate());
    vm1.Method(vm2.Method());
    vm1.Courses(vm2.Courses());
}

function MapCourseVM(vm1, vm2) {
    vm1.Id(vm2.Id());
    vm1.CourseName(vm2.CourseName());
    vm1.LeadTeacher(vm2.LeadTeacher());
    vm1.Points(vm2.Points());
}

function MapMarkVM(vm1, vm2) {
    vm1.Id(vm2.Id());
    vm1.StudentId(vm2.StudentId());
    vm1.CourseId(vm2.CourseId());
    vm1.AddedDate(vm2.AddedDate());
    vm1.Evaluation(vm2.Evaluation());
}

function GetStudents() {
    GetFromApi("students", null, studentsList);
    ClearFilter(studentsList);
}

function GetCourses() {
    GetFromApi("courses", null, coursesList);
    ClearFilter(coursesList);
}

function GetStudentCourses(index) {
    GetFromApi("students", "/" + index + "/courses", studentCoursesList);
    ClearFilter(studentCoursesList);
}

function GetMarks() {
    GetFromApi("marks", null, marksList);
    ClearFilter(marksList);
}

function GetStudentCoursesMark(studentId, courseId) {
    studentCourseMarkList.StudentIdFilter = studentId;
    studentCourseMarkList.CourseIdFilter = courseId;
    GetFromApi("marks", null, studentCourseMarkList);
    ClearFilter(studentCourseMarkList);
}

function GetStudentDetails(index) {
    var vm = studentsList.list()[index];
    MapStudentVM(studentDetails, vm);
}

function GetCoursesDetails(id) {
    var vm = coursesList.list()[id];
    MapCourseVM(courseDetails, vm);
}

function GetMarksEditor(id) {
    var vm = marksList.list()[id];
    MapMarkVM(marksDetails, vm);
}

function GetStudentEditor(index) {
    var vm = studentsList.list()[index];
    MapStudentVM(studentEditor, vm);
}

function GetCoursesEditor(id) {
    var vm = coursesList.list()[id];
    MapCourseVM(courseEditor, vm);
}

function GetMarksDetails(id) {
    var vm = marksList.list()[id];
    MapMarkVM(marksEditor, vm);
}

function ClearStudentEditor() {
    MapStudentVM(studentEditor, new StudentModel());
}

function ClearCourseEditor() {
    MapCourseVM(courseEditor, new CourseModel());
}

function ClearMarksEditor() {
    MapMarkVM(marksEditor, new MarkModel());
}

function CreateUpdateStudent() {
    if (studentEditor.Id()) {
        UpdateObject("students", null, studentEditor, studentEditor.Index());
    }
    else {
        CreateObject("students", null, studentEditor);
    }
}

function CreateUpdateCourse() {
    if (courseEditor.Id()) {
        UpdateObject("courses", null, courseEditor, courseEditor.Id());
    }
    else {
        CreateObject("courses", null, courseEditor);
    }
}

function CreateUpdateMark() {
    if (studentEditor.Id()) {
        UpdateObject("marks", null, marksEditor, marksEditor.Id());
    }
    else {
        CreateObject("marks", null, marksEditor);
    }
}

function AddStudentCourse(id) {
    UpdateObject("students", "/" + id + "courses/" + courseEditor.CourseName, courseEditor, id);

}

function DeleteStudent(index) {
    var respond = confirm("Czy na pewno chcesz usun¹æ?");
    if (respond) {
        DeleteObject("students/" + index);
        GetStudents();
    }
}

function DeleteCourse(id) {
    var respond = confirm("Czy na pewno chcesz usun¹æ?");
    if (respond) {
        DeleteObject("courses/" + id);
        GetCourses();
    }
}

function DeleteMark(id) {
    var respond = confirm("Czy na pewno chcesz usun¹æ?");
    if (respond) {
        DeleteObject("marks/" + id);
        GetMarks();
    }
}

function AddParameter(parameterString, propertyName, value) {
    var result = "";

    if (!value) {
        return parameterString;
    }

    if (!parameterString) {
        result = "?";
    }
    else {
        result = parameterString;
    }

    if (result == "?") {
        result = result + propertyName + "=" + value;
    }
    else {
        result = result + "&" + propertyName + "=" + value;
    }

    return result;
}


function GetFromApi(controller, method, vm) {
    if (!method) {
        method = "";
    }

    var parameters = "";
    parameters = AddParameter(parameters, "id", vm.IndexFilter);
    parameters = AddParameter(parameters, "name", vm.NameFilter)
    parameters = AddParameter(parameters, "surname", vm.SurnameFilter);
    parameters = AddParameter(parameters, "birthdate", vm.BirthDateFromFilter);
    parameters = AddParameter(parameters, "method", vm.MethodFilter);

    
    parameters = AddParameter(parameters, "courseName", vm.CourseNameFilter);
    parameters = AddParameter(parameters, "leadTeacher", vm.LeadTeacherFilter);
    parameters = AddParameter(parameters, "points", vm.PointsFilter);
    parameters = AddParameter(parameters, "method", vm.MethodFilter);

    parameters = AddParameter(parameters, "studentId", vm.StudentIdFilter);
    parameters = AddParameter(parameters, "courseId", vm.CourseIdFilter);
    parameters = AddParameter(parameters, "addedDate", vm.AddedDateFilter);
    parameters = AddParameter(parameters, "evaluation", vm.EvaluationFilter);

    var newUrl = protocol + serverAddress + port + apiPath + controller + method + parameters;

    $.ajax({
        url: newUrl,
        method: "GET",
        async: false,
        "accept": "application/json",
        success: function (data) {
            ko.mapping.fromJS(data, {}, vm.list);
        },
        error: function (error) {
            var errorVm = new ErrorModel();
            var obj = JSON.parse(error.responseText);
            ko.mapping.fromJS(obj, {}, errorVm);
            alert(errorVm.Message() + "\n" + errorVm.ExceptionMessage());
        }
    });

}

function CreateObject(controller, method, vm) {
    if (!method) {
        method = "";
    }

    var obj = ko.mapping.toJS(vm);
    var json = JSON.stringify(obj);

    $.ajax({
        url: protocol + serverAddress + port + apiPath + controller + method,
        method: "POST",
        async: false,
        data: json,
        contentType: "application/json",
        success: function (data) {
            alert("Create success!");
        },
        error: function (error) {
            var errorVM = new ErrorModel();
            var obj = JSON.parse(error.responseText);
            ko.mapping.fromJS(obj, {}, errorVM);
            alert(errorVM.Message() + "\n" + errorVM.ExceptionMessage());
        }
    });
}

function UpdateObject(controller, method, vm, id) {
    if (!method) {
        method = "";
    }

    var obj = ko.mapping.toJS(vm);
    var json = JSON.stringify(obj);

    $.ajax({
        url: protocol + serverAddress + port + apiPath + controller + method + "/" + id,
        method: "PUT",
        async: false,
        data: json,
        contentType: "application/json",
        success: function (data) {
            alert("Update success!");
        },
        error: function (error) {
            var errorVm = new ErrorModel();
            var obj = JSON.parse(error.responseText);
            ko.mapping.fromJS(obj, {}, errorVm);
            alert(errorVm.Message() + "\n" + errorVm.ExceptionMessage());
        }
    });
}


function DeleteObject(path) {
    $.ajax({
        url: protocol + serverAddress + port + apiPath + path,
        method: "DELETE",
        async: false,
        "contentType": "application/json",
        success: function (data) {
            alert("Delete success.");
        },
        error: function (error) {
            var errorVm = new ErrorModel();
            var obj = JSON.parse(error.responseText);
            ko.mapping.fromJS(obj, {}, errorVm);
            alert(errorVm.Message() + "\n" + errorVm.ExceptionMessage());
        }
    });
}


function ClearFilter(vm) {
    vm.IndexFilter("");
    vm.NameFilter("");
    vm.SurnameFilter("");
    vm.BirthdateFilter("");
    vm.MethodFilter("");

    vm.CourseNameFilter("");
    vm.LeadTeacherFilter("");
    vm.PointsFilter("");

    vm.StudentIdFilter("");
    vm.CourseIdFilter("");
    vm.AddedDateFilter("");
    vm.EvaluationFilter("");

}