pub fn load_input(file: &str) -> String {
    return std::fs::read_to_string(file).expect("expected to load project file");
}

pub fn split_into_rows_vec(full_text: String) -> Vec<String> {
    return full_text.lines().map(|s: &str| s.to_string()).collect();
}
