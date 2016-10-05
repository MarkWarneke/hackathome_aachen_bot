"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var http_1 = require('@angular/http');
require('rxjs/add/operator/toPromise');
var user_1 = require('./user');
var UserService = (function () {
    function UserService(http) {
        this.http = http;
        this.remote = 'http://shruggieuserrest.azurewebsites.de/api/users/'; // URL to web API
        this.local = 'api/users/'; // URL to web API
        this.url = this.local;
    }
    UserService.prototype.search = function (term) {
        return this.http
            .get("api/users/" + term)
            .map(function (r) { return r.json().data; });
    };
    UserService.prototype.getUser = function (term) {
        return this.http.get(this.url + term)
            .toPromise()
            .then(this.extractData)
            .catch(this.handleError);
    };
    UserService.prototype.extractData = function (res) {
        var body = res.json();
        console.log("body", body);
        console.log("new User", new user_1.User(body.id, body.chat_s, body.person));
        return new user_1.User(body.id, body.chat_s, body.person);
    };
    UserService.prototype.handleError = function (error) {
        // In a real world app, we might use a remote logging infrastructure
        // We'd also dig deeper into the error to get a better message
        var errMsg = (error.message) ? error.message :
            error.status ? error.status + " - " + error.statusText : 'Server error';
        console.error(errMsg); // log to console instead
        return Promise.reject(errMsg);
    };
    UserService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], UserService);
    return UserService;
}());
exports.UserService = UserService;
//# sourceMappingURL=user.service.js.map