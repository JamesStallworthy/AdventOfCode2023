use std::usize;

fn load_input() -> String {
    return std::fs::read_to_string("./day1/input.txt").expect("expected to load project file");
}

fn split_into_row(full_text: String) -> Vec<String> {
    return full_text.lines().map(|s: &str| s.to_string()).collect();
}

fn get_word_number(include_words: bool, row: &String, i: usize) -> Result<u32, &str> {
    let number_words: Vec<String> = vec![
        "one".to_string(),
        "two".to_string(),
        "three".to_string(),
        "four".to_string(),
        "five".to_string(),
        "six".to_string(),
        "seven".to_string(),
        "eight".to_string(),
        "nine".to_string(),
    ];

    if !include_words {
        return Err("Number check is turned off");
    }

    let remaining: String = row.chars().skip(i).collect();
    let offset: u32 = 1;
    for (i, nw) in number_words.iter().enumerate() {
        if remaining.starts_with(nw) {
            return Ok(u32::try_from(i).unwrap() + offset);
        }
    }

    return Err("Not a number");
}

fn find_first_and_last(row: String, include_words: bool) -> u32 {
    let mut first_num: Option<u32> = None;
    let mut last_num: Option<u32> = None;

    for (i, c) in row.chars().enumerate() {
        if c.is_numeric() {
            if first_num.is_none() {
                first_num = Some(c as u32 - '0' as u32);
            }
            last_num = Some(c as u32 - '0' as u32);
        } else {
            match get_word_number(include_words, &row, i) {
                Ok(number) => {
                    if first_num.is_none() {
                        first_num = Some(number);
                    }

                    last_num = Some(number);
                }
                _ => (),
            }
        }
    }

    if first_num.is_none() {
        return 0;
    }
    return (first_num.unwrap() * 10) + last_num.unwrap();
}

pub fn get_sum(rows: &Vec<String>, include_words: bool) -> u32 {
    let mut sum: u32 = 0;
    for row in rows.iter() {
        sum += find_first_and_last(row.to_string(), include_words);
    }

    sum
}

pub fn solve() {
    let rows = split_into_row(load_input());

    println!("{}", get_sum(&rows, false));
    println!("{}", get_sum(&rows, true));
}

#[test]
fn simple_test() {
    let rows = vec!["1a2".to_string()];

    assert_eq!(get_sum(&rows, false), 12);
}

#[test]
fn simple_test_2() {
    let rows = vec!["treb7uchet".to_string()];

    assert_eq!(get_sum(&rows, false), 77);
}

#[test]
fn simple_test_3() {
    let rows = vec!["xtwone3four".to_string()];

    assert_eq!(get_sum(&rows, true), 24);
}
