<template>
    <div>

       <input type="text" v-model="readyMessage" @keypress="sendMessage" />
    
    
        <div>
            <ul>
                <li v-for="(msg,index) in messages" :key="index">{{msg}}</li>
            </ul>
        </div> 

    </div>
  
</template>

<script>

import * as signalR from '@microsoft/signalr';
import axios from 'axios';

let connection;
export default {
    data() {
        return {
            readyMessage:"",
            messages:[],
            accessToken:"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoienNjIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkFkbWluIiwiaGFoYWgiXSwiZXhwIjoxNjcwOTk4NDc4fQ.cd_huBa3SWiMg_kYZZyyiI9i4eCW5WUdsyG6jJNO-xI",
        }
    },

    methods:{
        sendMessage(e){
            if (e.keyCode != 13) return;

            connection.invoke("SendPrivateMessage", "2",this.readyMessage);
        },
        
    },

    mounted(){
        const transport = signalR.HttpTransportType.WebSockets;
        connection = new signalR.HubConnectionBuilder()
                .withUrl("http://localhost:54877/chatHub", { accessTokenFactory: () => this.accessToken }).build();

            try {
                connection.start();
            } catch (err) {
                alert(err);
                return;
            }
            connection.on('ReceivePrivateMessage', msg => {
                state.messages.push(msg);
            });
            alert("登陆成功可以聊天了");
    }

   

}
</script>

<style>

</style>