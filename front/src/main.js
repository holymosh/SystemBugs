// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import Vuetify from 'vuetify'

Vue.config.productionTip = false
Vue.use(Vuetify)
/* eslint-disable no-new */
new Vue({
  el: '#app',
  render: h => h(App),
  components: { App },
  template: '<App/>',
  vuetify: new Vuetify()
})
