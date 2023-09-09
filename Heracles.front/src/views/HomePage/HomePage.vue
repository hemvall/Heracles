<template>
    <a class="button" @click="addCategoryOpen = !addCategoryOpen">Add a category</a>
    <div v-if="addCategoryOpen">
        <input v-model="label" type="text" />
        <button @click="createCategory">Valider</button>
        <button @click="addCategoryOpen = !addCategoryOpen">Fermer</button>
    </div>
    <div v-for="t in typesFromUser" :key="t.id">
        <h1 class="typeLabel">{{ t.label }}</h1>
        <button @click="editCategoryOpen = !editCategoryOpen">Modifier</button>
        <div v-if="editCategoryOpen">
            <input v-model="editLabel" type="text" />
            <button @click="editCategory(t.id)">Valider</button>
            <button @click="editCategoryOpen = !editCategoryOpen">Fermer</button>
        </div>
        <div class="exercisesContainer">
            <RouterLink to="/" v-for="e in exercisesFromType(t.id)" :key="e.id">
                <p>{{ e.label }}</p>
            </RouterLink>
        </div>
    </div>
</template>


<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.fetchData();
        this.userId = localStorage.getItem('userId')
        console.log(this.userId)
    },
    data() {
        return {
            exercises: [],
            types: [],
            userId: 99,
            label: '',
            editLabel: '',
            addCategoryOpen: false,
            editCategoryOpen: false,
        };
    },
    methods: {
        fetchData() {
            this.exercises = [];
            this.types = [];
            this.loading = true;

            fetch(`${this.$api}/exercisetype`)
                .then(r => r.json())
                .then(json => {
                    this.types = json;
                    this.loading = false;
                    return;
                });
            fetch(`${this.$api}/exercises`)
                .then(r => r.json())
                .then(json => {
                    this.exercises = json;
                    this.loading = false;
                    return;
                });
        },
        createCategory() {
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    label: this.label,
                    userId: this.userId
                })
            };
            fetch(`${this.$api}/exerciseType`, requestOptions)
                .then(response => {
                    if (response.ok) { alert("La catégorie a bien été créee") }
                    else { alert("La catégorie n'a pas été créee") }
                    this.fetchData()
                })
                .then(response => response.json())
            // .then(data => (this.postId = data.id));
        },
        editCategory(tId) {
            const requestOptions = {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    id: tId,
                    label: this.editLabel
                })
            };
            fetch(`${this.$api}/exerciseType/${tId}`, requestOptions)
                .then(response => {
                    if (response.ok) { alert("La catégorie a bien été modifiée") }
                    else { alert("La catégorie n'a pas été modifiée") }
                    this.fetchData()
                })
                .then(response => response.json())
            // .then(data => (this.postId = data.id));
        },
        exercisesFromType(tId) {
            return this.exercises.filter(e => e.typeId == tId && e.userId == localStorage.getItem('userId'))
        }
    },
    computed: {
        typesFromUser() {
            return this.types.filter(e => e.userId == localStorage.getItem('userId'))
        },
    }
}
);
</script>

<style scoped lang="scss">
@import url('./HomePage.scss');

template {
    margin: 0 10%;
}

* {
    font-family: Arial, Helvetica, sans-serif;
}
</style>