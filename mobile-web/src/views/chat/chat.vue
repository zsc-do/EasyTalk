<template>
  <div>
        <van-cell-group v-for="(msg,index) in messages" :key="index">
            <van-cell  :value="msg" />
        </van-cell-group>


        <van-field v-model="readyMessage" label="文本" 
        placeholder="请输入消息" 
        @keypress="sendMessage"/>

        <!-- <input type="text" v-model="readyMessage" @keypress="sendMessage" /> -->
  </div>


  
</template>

<script>
import * as signalR from '@microsoft/signalr';

let connection;
export default {
    data() {
        return {
            readyMessage:"",
            messages:[],
            accessToken:""
        }
    },

    methods:{
        sendMessage(e){
            if (e.keyCode != 13) return;

            let userId = this.$store.state.UserId;
            let curConnectUserId = this.$store.state.curConnectUserId;
            console.log(userId,curConnectUserId)
            connection.invoke("SendPrivateMessage", userId + "",curConnectUserId + "",this.readyMessage);
        },
        
    },

    mounted(){

        this.accessToken = localStorage.getItem('token');

        const transport = signalR.HttpTransportType.ServerSentEvents
                const options = { skipNegotiation: false, transport: transport };
                options.accessTokenFactory = () => this.accessToken;
                connection = new signalR.HubConnectionBuilder()
                    .withUrl('http://localhost:54877/chatHub', options)
                    .withAutomaticReconnect().build();




        

            try {
                connection.start();
            } catch (err) {
                alert(err);
                return;
            }
            connection.on('ReceivePrivateMessage', (srcUserId, time, message) => {
                
                if (srcUserId != this.$store.state.curConnectUserId 
                    && this.$store.state.UserId != srcUserId) {
                console.log("阻断了")
                return;
                }
                this.messages.push(message);
            });
            //alert("登陆成功可以聊天了");
    }
}
</script>

<style>

</style>