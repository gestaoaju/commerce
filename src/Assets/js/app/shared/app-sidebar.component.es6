import Vue from 'vue';

export const Sidebar = Vue.extend({
    methods: {
        showSidebar() {
            console.log('BLABLA');
        }
    },
    mouted() {
        console.log('mounted');
    }
});
