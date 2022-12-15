<template>
  <div>
    <van-nav-bar
        title="EasyTalk"
        left-text="返回登录"
        left-arrow
        @click-left="onClickLeft"
        />

        <router-view/>

        <van-tabbar v-model="active">
          <van-tabbar-item to="/layout/index" icon="home-o">好友列表</van-tabbar-item>
          <van-tabbar-item to="/layout/search" icon="search">搜索</van-tabbar-item>
        </van-tabbar>
  </div>
</template>

<script>
export default {
  data(){
    return{
      active:true
    }
  },
    methods:{
        onClickLeft(){
            
        }
    }
}
</script>

<style>

</style>