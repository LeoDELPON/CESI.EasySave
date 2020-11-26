import React, {Component} from 'react';
import axios from 'axios';
class Request_API extends Component {  
    constructor(API_URL){
        super();
        this.API_URL = API_URL;
    }
    getAllWaitingForms() {
        const REQ_URL = this.API_URL + "/getAllWaitingForms";
        return this.axiosRequest("get", REQ_URL);
    }
    postFormByID(ID){
        const REQ_URL = this.API_URL + "/postFormByID";
        return this.axiosRequest('post', URL, "{'Content-Type': 'application/json'}", JSON.stringify({ID : ID}));
    }
    deleteFormByID(ID){
        const REQ_URL = this.API_URL + "/deleteFormByID";
        return this.axiosRequest('delete', URL, "{'Content-Type': 'application/json'}", JSON.stringify({ID : ID}));
    }
    axiosRequest(http_method, URL, headers, ...data){
        this.http_request = {
            "get" : axios({method: 'get', url: URL, headers: headers}),
            "post" : axios({method: 'post', url: URL, data: data, headers: headers}),
            "delete" : axios({method: 'delete', url: URL, data: data, headers: headers})
        };
        this.http_request[http_method].then((resp) => {
            return resp;
        }).catch((err) => {
            console.log(err)
        });
    }
};
export default Request_API;