<template>
  <div>
        <van-cell-group v-for="fri in friends" :key="fri.id" @click="chatView(fri.id)">
            <van-cell :title="fri.userName" :value="fri.userName" />
        </van-cell-group>
  </div>
</template>

<script>
export default {
    data(){
        return {
            friends:[]
        }
    },
    methods:{
        chatView(destUserId){
            this.$store.commit('setCurConnectUserId',destUserId);

            this.$router.push('/chat')
        }
    },
    mounted(){

        this.$http.post("http://localhost:54877/Friend/GetUserFriend?curId=" + this.$store.state.UserId)
      .then((res)=>{

        console.log(res.data)
        this.friends = res.data;
      })
    }

}
</script>

<style>

</style>