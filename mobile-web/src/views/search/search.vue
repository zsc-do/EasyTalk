<template>
  <div>
    <van-search v-model="value" placeholder="请输入搜索关键词"
    @search="onSearch" />

    <van-cell-group v-for="user in searchUser" :key="user.id">
            <van-cell :title="user.userName" :value="user.userName">
            <van-button plain type="primary" @click="addFriend(user.id,$event)">添加好友</van-button>
            </van-cell>
            
    </van-cell-group>
  </div>
</template>

<script>
export default {

    data() {
    return {
      value: '',
      searchUser:[]
    };
  },

  methods: {
    onSearch() {
        this.$http.post("http://localhost:54877/Friend/SearchUsers?username=" + this.value)
      .then((res)=>{

        console.log(res.data)
        this.searchUser = res.data;
      })
    },
    addFriend(userId,obj){
      this.$http.post("http://localhost:54877/Friend/AddFriend?curUserId=" + this.$store.state.UserId
      + "&" + "userId=" + userId)
      .then((res)=>{

        console.log(obj)
      }) 
    }
  }

}
</script>

<style>

</style>