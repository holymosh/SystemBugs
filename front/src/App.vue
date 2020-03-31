<template>
  <div id="app">
    <BugData/>
  </div>
</template>

<script>
import BugData from './components/BugData'

export default {
  name: 'App',
  components: {
    BugData
  }
}
</script>

<style >
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
