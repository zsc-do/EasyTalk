<template>
  <div>
    <van-form @submit="onSubmit">
  <van-field
    v-model="username"
    name="用户名"
    label="用户名"
    placeholder="用户名"
    :rules="[{ required: true, message: '请填写用户名' }]"
  />
  <van-field
    v-model="password"
    type="password"
    name="密码"
    label="密码"
    placeholder="密码"
    :rules="[{ required: true, message: '请填写密码' }]"
  />
  <div style="margin: 16px;">
    <van-button round block type="info" native-type="submit">提交</van-button>
  </div>
</van-form>
  </div>
</template>

<script>

export default {
    data() {
    return {
      username: '',
      password: '',
    };
  },
  methods: {
    onSubmit() {
      this.$http.post("http://localhost:54877/Login/doLogin",{
        UserName:this.username,
        Password:this.password
      })
      .then((res)=>{

        localStorage.setItem("token", (res.data)[1]);

        this.$store.commit('setUserId',(res.data)[0]);
        
        this.$router.push('/layout/index')
      })
    },
  }
}
</script>

