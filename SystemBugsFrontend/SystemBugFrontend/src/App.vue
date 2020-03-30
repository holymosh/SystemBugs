<template>
  <div id="app">
    <BugComponent/>
  </div>
</template>

<script>
import BugComponent from './components/BugComponent'

export default {
  name: 'App',
  components: {
    BugComponent
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
