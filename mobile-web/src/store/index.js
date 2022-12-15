import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    UserId:"",
    curConnectUserId:""
  },
  getters: {
  },
  mutations: {
    setUserId(state,id){
      state.UserId = id;
    },
    setCurConnectUserId(state,id){
      state.curConnectUserId = id;
    }
  },
  actions: {
  },
  modules: {
  }
})
